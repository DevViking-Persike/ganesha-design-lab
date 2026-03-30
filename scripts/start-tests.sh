#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"

# shellcheck source=common.sh
source "${SCRIPT_DIR}/common.sh"

require_command dotnet

load_env_files

TEST_PROJECT="${TEST_PROJECT:-tests/Ganesha.DesignLab.Shared.Tests/Ganesha.DesignLab.Shared.Tests.csproj}"
CONFIGURATION="${CONFIGURATION:-Debug}"

print_banner \
  "Ganesha Design Lab Tests" \
  "Projeto de testes: ${TEST_PROJECT}" \
  "Configuration: ${CONFIGURATION}"

cd "${PROJECT_ROOT}"
dotnet test "${TEST_PROJECT}" --configuration "${CONFIGURATION}"
