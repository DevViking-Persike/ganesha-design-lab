#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"

# shellcheck source=common.sh
source "${SCRIPT_DIR}/common.sh"

require_command docker
require_command ssh
require_command git

DOCKER_IMAGE="${DOCKER_IMAGE:-victorpersike.dev.br/ganesha-ds/web:latest}"
DOCKER_PLATFORM="${DOCKER_PLATFORM:-linux/arm64}"
K8S_HOST="${K8S_HOST:-oracle-a1}"
INFRA_ROOT="${INFRA_ROOT:-/Volumes/HDX/Dev/ia-trainner-microservico}"
APP_MANIFEST="${APP_MANIFEST:-${INFRA_ROOT}/k8s/apps/ganesha-ds-web.yaml}"
INGRESS_MANIFEST="${INGRESS_MANIFEST:-${INFRA_ROOT}/k8s/networking/ganesha-ds-web-ingress.yaml}"
K8S_NAMESPACE="${K8S_NAMESPACE:-apps}"
DEPLOYMENT_NAME="${DEPLOYMENT_NAME:-ganesha-ds-web}"

SKIP_BUILD=false
SKIP_PUSH=false
SKIP_APPLY=false

while [[ $# -gt 0 ]]; do
  case "$1" in
    --skip-build) SKIP_BUILD=true; shift ;;
    --skip-push) SKIP_PUSH=true; shift ;;
    --skip-apply) SKIP_APPLY=true; shift ;;
    -h|--help)
      cat <<'EOF'
Uso: ./scripts/deploy-web.sh [--skip-build] [--skip-push] [--skip-apply]
EOF
      exit 0
      ;;
    *)
      error "Opcao desconhecida: $1"
      exit 1
      ;;
  esac
done

print_banner \
  "Ganesha Design Lab Deploy" \
  "Imagem: ${DOCKER_IMAGE}" \
  "Plataforma: ${DOCKER_PLATFORM}" \
  "Cluster host: ${K8S_HOST}" \
  "Namespace: ${K8S_NAMESPACE}"

if [[ "${SKIP_BUILD}" == false ]]; then
  info "Construindo imagem"
  docker build --platform "${DOCKER_PLATFORM}" -t "${DOCKER_IMAGE}" "${PROJECT_ROOT}"
  success "Imagem criada"
else
  warn "Pulando build"
fi

if [[ "${SKIP_PUSH}" == false ]]; then
  info "Publicando imagem no registry"
  docker push "${DOCKER_IMAGE}"
  success "Imagem publicada"
else
  warn "Pulando push"
fi

if [[ "${SKIP_APPLY}" == false ]]; then
  info "Aplicando manifests no cluster"
  ssh "${K8S_HOST}" "sudo kubectl apply -f -" < "${APP_MANIFEST}"
  ssh "${K8S_HOST}" "sudo kubectl apply -f -" < "${INGRESS_MANIFEST}"
  ssh "${K8S_HOST}" "sudo kubectl rollout restart deployment/${DEPLOYMENT_NAME} -n ${K8S_NAMESPACE}"
  ssh "${K8S_HOST}" "sudo kubectl rollout status deployment/${DEPLOYMENT_NAME} -n ${K8S_NAMESPACE} --timeout=180s"
  success "Deploy aplicado no cluster"
else
  warn "Pulando apply"
fi
