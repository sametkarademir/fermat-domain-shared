#!/bin/bash

set -e

# Project file path
PROJECT_FILE="src/Fermat.Domain.Shared/Fermat.Domain.Shared.csproj"

# Version type parameter (patch, minor, major)
VERSION_TYPE=${1:-patch}

echo "🚀 Starting release process..."

# Get the latest tag from Git
LATEST_TAG=$(git describe --tags $(git rev-list --tags --max-count=1) 2>/dev/null || echo "")
echo "📍 Latest tag: $LATEST_TAG"

# If no tags exist, start with v0.0.1
if [ -z "$LATEST_TAG" ] || [ -z "$(git tag -l)" ]; then
    echo "⚠️  No tags found. Starting with v0.0.1"
    NEW_VERSION="v0.0.1"
    # Force commit even if version is the same for initial release
    FORCE_COMMIT=true
else
    # Increment version
    IFS='.' read -r major minor patch <<< "${LATEST_TAG#v}"
    
    case $VERSION_TYPE in
        major)
            NEW_VERSION="v$((major + 1)).0.0"
            ;;
        minor)
            NEW_VERSION="v$major.$((minor + 1)).0"
            ;;
        patch)
            NEW_VERSION="v$major.$minor.$((patch + 1))"
            ;;
        *)
            echo "❌ Invalid version type. Use: major, minor, or patch"
            exit 1
            ;;
    esac
fi

echo "🆕 New version: $NEW_VERSION"

# Update <Version> field in .csproj file
echo "📝 Updating version in $PROJECT_FILE"
if [[ "$OSTYPE" == "darwin"* ]]; then
    # For macOS
    sed -i '' -E "s|<Version>.*</Version>|<Version>${NEW_VERSION#v}</Version>|" "$PROJECT_FILE"
else
    # For Linux
    sed -i -E "s|<Version>.*</Version>|<Version>${NEW_VERSION#v}</Version>|" "$PROJECT_FILE"
fi

# Verify the change
echo "✅ Version updated in project file:"
grep -n "<Version>" "$PROJECT_FILE"

# Build and package .NET
echo "🔨 Building project..."
dotnet build "$PROJECT_FILE" -c Release

echo "📦 Packing..."
dotnet pack "$PROJECT_FILE" -c Release -o nupkg --no-build /p:PackageVersion="${NEW_VERSION#v}"

echo "📋 Generated packages:"
ls -la nupkg/

# Check Git status or force commit for initial release
echo "🔍 Checking git status and FORCE_COMMIT: $FORCE_COMMIT"
if [ -n "$(git status --porcelain)" ]; then
    echo "📤 Committing changes..."
    git add "$PROJECT_FILE"
    git commit -m "chore: bump version to ${NEW_VERSION}"
    git tag "$NEW_VERSION"
    git push origin main --tags
    echo "🎉 Version bumped and pushed. CI will now handle publish."
elif [ "$FORCE_COMMIT" = "true" ]; then
    echo "📤 Creating initial tag and pushing..."
    git tag "$NEW_VERSION"
    git push origin main --tags
    echo "🎉 Initial tag created and pushed. CI will now handle publish."
else
    echo "⚠️  No changes to commit."
fi

echo "✅ Release process completed!"
echo "🏷️  Tagged version: $NEW_VERSION"
