#!/bin/bash
set -euo pipefail

# Only run in remote (cloud) environment
if [ "${CLAUDE_CODE_REMOTE:-}" != "true" ]; then
  exit 0
fi

# Install .NET 6 SDK (supports OpenSSL 3.x on modern Linux, can build netcoreapp3.1 targets)
if ! command -v dotnet &>/dev/null || ! dotnet --list-sdks 2>/dev/null | grep -q "^6\.0"; then
  echo "Installing .NET 6 SDK..."
  apt-get update -qq
  apt-get install -y -qq wget

  wget -q https://dot.net/v1/dotnet-install.sh -O /tmp/dotnet-install.sh
  chmod +x /tmp/dotnet-install.sh
  /tmp/dotnet-install.sh --channel 6.0 --install-dir /usr/share/dotnet
  ln -sf /usr/share/dotnet/dotnet /usr/local/bin/dotnet
  rm /tmp/dotnet-install.sh
fi

# Set environment variables for .NET
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1

# Persist environment for the session
if [ -n "${CLAUDE_ENV_FILE:-}" ]; then
  echo 'export PATH="/usr/share/dotnet:$PATH"' >> "$CLAUDE_ENV_FILE"
  echo 'export DOTNET_ROOT="/usr/share/dotnet"' >> "$CLAUDE_ENV_FILE"
  echo 'export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1' >> "$CLAUDE_ENV_FILE"
  echo 'export DOTNET_CLI_TELEMETRY_OPTOUT=1' >> "$CLAUDE_ENV_FILE"
fi

# Restore NuGet packages
cd "$CLAUDE_PROJECT_DIR"
dotnet restore ClassLibrary4.sln

echo ".NET 6 SDK and dependencies installed successfully."
