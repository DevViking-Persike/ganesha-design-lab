#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"

# shellcheck source=common.sh
source "${SCRIPT_DIR}/common.sh"

require_command dotnet
require_command curl
require_command lsof

load_env_files

FRONTEND_PORT="${FRONTEND_PORT:-5169}"
FRONTEND_HOST="${FRONTEND_HOST:-localhost}"
FRONTEND_SCHEME="${FRONTEND_SCHEME:-http}"
ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"
FRONTEND_URL="${FRONTEND_SCHEME}://${FRONTEND_HOST}:${FRONTEND_PORT}"
HEALTHCHECK_URL="${HEALTHCHECK_URL:-${FRONTEND_URL}/}"
WEB_PROJECT="${WEB_PROJECT:-src/Ganesha.DesignLab.Web/Ganesha.DesignLab.Web.csproj}"

print_banner \
  "Ganesha Design Lab Frontend" \
  "Projeto: ${WEB_PROJECT}" \
  "URL: ${FRONTEND_URL}" \
  "Environment: ${ASPNETCORE_ENVIRONMENT}"

if port_in_use "${FRONTEND_PORT}"; then
  warn "Porta ${FRONTEND_PORT} já está em uso."
  if [[ "${KILL_EXISTING_PORT:-0}" == "1" ]]; then
    kill_port "${FRONTEND_PORT}"
  else
    error "Defina KILL_EXISTING_PORT=1 para encerrar o processo atual."
    exit 1
  fi
fi

export ASPNETCORE_ENVIRONMENT
export ASPNETCORE_URLS="${FRONTEND_URL}"

info "Iniciando frontend"
(cd "${PROJECT_ROOT}" && dotnet run --project "${WEB_PROJECT}" --no-launch-profile) &
APP_PID=$!

cleanup() {
  if kill -0 "${APP_PID}" >/dev/null 2>&1; then
    warn "Encerrando frontend (${APP_PID})"
    kill "${APP_PID}" >/dev/null 2>&1 || true
    wait "${APP_PID}" 2>/dev/null || true
  fi
}

trap cleanup EXIT INT TERM

wait_for_http "${HEALTHCHECK_URL}" 45 1
success "Frontend disponível em ${FRONTEND_URL}"

wait "${APP_PID}"
