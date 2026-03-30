#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "${SCRIPT_DIR}/../.." && pwd)"
ROOT_SCRIPTS_DIR="${PROJECT_ROOT}/scripts"

# shellcheck source=../common.sh
source "${ROOT_SCRIPTS_DIR}/common.sh"

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
LOG_FILE="${ROOT_SCRIPTS_DIR}/.logs/frontend.log"
PID_FILE="${ROOT_SCRIPTS_DIR}/.pids/frontend.pid"

print_banner \
  "Ganesha Design Lab Local Stack" \
  "Frontend: ${FRONTEND_URL}" \
  "Health check: ${HEALTHCHECK_URL}" \
  "Logs: ${LOG_FILE}"

kill_port "${FRONTEND_PORT}"

export ASPNETCORE_ENVIRONMENT
export ASPNETCORE_URLS="${FRONTEND_URL}"

info "Subindo frontend em background"
(
  cd "${PROJECT_ROOT}"
  dotnet run --project "${WEB_PROJECT}" --no-launch-profile
) >"${LOG_FILE}" 2>&1 &

APP_PID=$!
echo "${APP_PID}" > "${PID_FILE}"

cleanup() {
  if [[ -f "${PID_FILE}" ]]; then
    rm -f "${PID_FILE}"
  fi

  if kill -0 "${APP_PID}" >/dev/null 2>&1; then
    warn "Encerrando frontend (${APP_PID})"
    kill "${APP_PID}" >/dev/null 2>&1 || true
    wait "${APP_PID}" 2>/dev/null || true
  fi
}

trap cleanup EXIT INT TERM

wait_for_http "${HEALTHCHECK_URL}" 45 1
success "Stack local pronta"
info "Acompanhe os logs com: tail -f ${LOG_FILE}"

wait "${APP_PID}"
