#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"
LOG_DIR="${SCRIPT_DIR}/.logs"
PID_DIR="${SCRIPT_DIR}/.pids"

mkdir -p "${LOG_DIR}" "${PID_DIR}"

if [[ -t 1 ]]; then
  COLOR_INFO='\033[1;34m'
  COLOR_SUCCESS='\033[1;32m'
  COLOR_WARN='\033[1;33m'
  COLOR_ERROR='\033[1;31m'
  COLOR_RESET='\033[0m'
else
  COLOR_INFO=''
  COLOR_SUCCESS=''
  COLOR_WARN=''
  COLOR_ERROR=''
  COLOR_RESET=''
fi

info() {
  printf "%b[INFO]%b %s\n" "${COLOR_INFO}" "${COLOR_RESET}" "$*"
}

success() {
  printf "%b[OK]%b %s\n" "${COLOR_SUCCESS}" "${COLOR_RESET}" "$*"
}

warn() {
  printf "%b[WARN]%b %s\n" "${COLOR_WARN}" "${COLOR_RESET}" "$*"
}

error() {
  printf "%b[ERROR]%b %s\n" "${COLOR_ERROR}" "${COLOR_RESET}" "$*" >&2
}

require_command() {
  if ! command -v "$1" >/dev/null 2>&1; then
    error "Comando obrigatório não encontrado: $1"
    exit 1
  fi
}

load_env_files() {
  local env_file

  for env_file in "${PROJECT_ROOT}/.env" "${PROJECT_ROOT}/.env.development"; do
    if [[ -f "${env_file}" ]]; then
      info "Carregando $(basename "${env_file}")"
      set -a
      # shellcheck disable=SC1090
      source "${env_file}"
      set +a
    fi
  done
}

require_env() {
  local var_name="$1"
  if [[ -z "${!var_name:-}" ]]; then
    error "Variável obrigatória ausente: ${var_name}"
    exit 1
  fi
}

port_in_use() {
  local port="$1"
  lsof -ti "tcp:${port}" >/dev/null 2>&1
}

kill_port() {
  local port="$1"
  local pids

  pids="$(lsof -ti "tcp:${port}" 2>/dev/null || true)"
  if [[ -n "${pids}" ]]; then
    warn "Encerrando processo(s) na porta ${port}: ${pids//$'\n'/ }"
    while IFS= read -r pid; do
      [[ -n "${pid}" ]] && kill "${pid}" >/dev/null 2>&1 || true
    done <<< "${pids}"
  fi
}

wait_for_http() {
  local url="$1"
  local retries="${2:-30}"
  local delay="${3:-1}"
  local attempt

  for ((attempt = 1; attempt <= retries; attempt++)); do
    if curl -fsS "${url}" >/dev/null 2>&1; then
      success "Health check OK: ${url}"
      return 0
    fi

    info "Aguardando ${url} (${attempt}/${retries})"
    sleep "${delay}"
  done

  error "Health check falhou: ${url}"
  return 1
}

print_banner() {
  local title="$1"
  shift

  printf "\n%s\n" "========================================"
  printf "%s\n" "${title}"
  while (($#)); do
    printf "%s\n" "$1"
    shift
  done
  printf "%s\n\n" "========================================"
}
