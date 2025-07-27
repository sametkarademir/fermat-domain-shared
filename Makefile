.PHONY: release release-patch release-minor release-major help

# Default target
help:
	@echo "Available commands:"
	@echo "  make release       - Create a patch release (default)"
	@echo "  make release-patch - Create a patch release (x.x.+1)"
	@echo "  make release-minor - Create a minor release (x.+1.0)"
	@echo "  make release-major - Create a major release (+1.0.0)"
	@echo "  make build         - Build the project"
	@echo "  make test          - Run tests"
	@echo "  make clean         - Clean build artifacts"

# Release commands
release: release-patch

release-patch:
	@echo "Creating patch release..."
	@./scripts/release.sh patch

release-minor:
	@echo "Creating minor release..."
	@./scripts/release.sh minor

release-major:
	@echo "Creating major release..."
	@./scripts/release.sh major

# Development commands
build:
	@echo "Building project..."
	@dotnet build --configuration Release

test:
	@echo "Running tests..."
	@dotnet test --configuration Release

clean:
	@echo "Cleaning build artifacts..."
	@dotnet clean
	@rm -rf nupkg/
	@rm -rf src/*/bin/
	@rm -rf src/*/obj/ 