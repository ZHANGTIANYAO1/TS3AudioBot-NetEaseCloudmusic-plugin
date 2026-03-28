#!/bin/bash
set -euo pipefail

# Only run in remote (cloud) environment
if [ "${CLAUDE_CODE_REMOTE:-}" != "true" ]; then
  exit 0
fi

# Install .NET 8 SDK if not available
if ! command -v dotnet &>/dev/null || ! dotnet --list-sdks 2>/dev/null | grep -q "^8\.0"; then
  echo "Installing .NET 8 SDK..."
  apt-get update -qq
  apt-get install -y -qq wget

  wget -q https://dot.net/v1/dotnet-install.sh -O /tmp/dotnet-install.sh
  chmod +x /tmp/dotnet-install.sh
  /tmp/dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet
  ln -sf /usr/share/dotnet/dotnet /usr/local/bin/dotnet
  rm /tmp/dotnet-install.sh
fi

# Persist environment for the session
if [ -n "${CLAUDE_ENV_FILE:-}" ]; then
  echo 'export PATH="/usr/share/dotnet:$PATH"' >> "$CLAUDE_ENV_FILE"
  echo 'export DOTNET_ROOT="/usr/share/dotnet"' >> "$CLAUDE_ENV_FILE"
  echo 'export DOTNET_CLI_TELEMETRY_OPTOUT=1' >> "$CLAUDE_ENV_FILE"
fi

export DOTNET_CLI_TELEMETRY_OPTOUT=1

# Restore NuGet packages
cd "$CLAUDE_PROJECT_DIR"
dotnet restore ClassLibrary4.sln

echo ".NET 8 SDK and dependencies installed successfully."
