#!/bin/bash

set -e

# Project file path
PROJECT_FILE="src/Fermat.Domain.Shared/Fermat.Domain.Shared.csproj"

# Version type parameter (patch, minor, major)
VERSION_TYPE=${1:-patch}

echo "🚀 Starting release process..."

# Fetch latest changes from remote
echo "🔄 Fetching latest changes from remote..."
git fetch origin

# Check if we're on the latest commit
LOCAL_COMMIT=$(git rev-parse HEAD)
REMOTE_COMMIT=$(git rev-parse origin/main)

if [ "$LOCAL_COMMIT" != "$REMOTE_COMMIT" ]; then
    echo "⚠️  Local branch is not up to date with remote. Pulling changes..."
    git pull origin main
fi

# Get the latest tag from Git
LATEST_TAG=$(git describe --tags $(git rev-list --tags --max-count=1) 2>/dev/null || echo "")
echo "📍 Latest tag: $LATEST_TAG"

# Check if current commit already has a tag
CURRENT_COMMIT=$(git rev-parse HEAD)
EXISTING_TAG=$(git tag --points-at $CURRENT_COMMIT | head -n 1)

if [ -n "$EXISTING_TAG" ]; then
    echo "⚠️  Current commit already has tag: $EXISTING_TAG"
    echo "❌ Release process aborted. No new tag needed."
    exit 0
fi

# If no tags exist, start with v0.0.1
if [ -z "$LATEST_TAG" ] || [ -z "$(git tag -l)" ]; then
    echo "⚠️  No tags found. Starting with v0.0.1"
    NEW_VERSION="v0.0.1"
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

# Always commit and tag if we've made changes to the project file
echo "📤 Committing version change..."
git add "$PROJECT_FILE"

# Check if there are actual changes to commit
if git diff --staged --quiet; then
    echo "⚠️  No version changes detected in project file."
    echo "🏷️  Creating tag anyway for current commit..."
    git tag "$NEW_VERSION"
    git push origin --tags
else
    echo "📝 Committing version bump..."
    git commit -m "chore: bump version to ${NEW_VERSION}"
    git tag "$NEW_VERSION"
    git push origin main --tags
fi

echo "🎉 Release process completed!"
echo "🏷️  Tagged version: $NEW_VERSION"
echo "🚀 GitHub Actions should now trigger with tag: $NEW_VERSION"