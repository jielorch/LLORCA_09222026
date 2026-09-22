# Docker Setup Guide - FileChecker Application

## Step-by-Step Docker Commands

### Step 1: Build Docker Image

```powershell
docker build -t filechecker:latest .
```

**Verifies build succeeded:**
```powershell
docker images | findstr filechecker
```

---

### Step 2: Start Containers (Foreground)

```powershell
docker-compose up
```

**Expected output:**
```
[+] Running 2/2
 ✔ Container filechecker-db   Running
 ✔ Container filechecker-api  Running
```

---

### Step 3: Verify Containers are Running

```powershell
docker-compose ps
```

**Should show:**
```
NAME               STATUS
filechecker-db    Up X minutes
filechecker-api   Up X minutes
```

---

### Step 4: Access the Application

**Swagger UI:**
```
http://localhost:8080/swagger
```

**API Health Check:**
```powershell
curl -X GET "http://localhost:8080/api/file/record" `
  -Headers @{"X-API-KEY" = "Your-Super-Secret-Local-Development-Key-12345"}
```

---

### Step 5: Stop Containers

```powershell
docker-compose down
```

---

### Step 6: Start Containers (Background)

```powershell
docker-compose up -d
```

**To see logs:**
```powershell
docker-compose logs -f
```

---

### Step 7: Rebuild After Code Changes

```powershell
docker-compose down
docker-compose up --build
```

---

## Common Docker Commands

```powershell
# View logs (all services)
docker-compose logs -f

# View logs (specific service)
docker-compose logs -f webapi
docker-compose logs -f sqlserver

# Execute command in container
docker-compose exec webapi bash
docker-compose exec webapi dotnet ef database update

# Remove stopped containers
docker-compose down

# Remove containers and volumes (deletes database)
docker-compose down -v

# List all containers
docker ps -a

# List images
docker images

# Remove unused images
docker image prune
```

---

## Troubleshooting

### Port Already in Use

```powershell
# Find process using port 8080
netstat -ano | findstr :8080

# Kill the process (replace PID)
taskkill /PID <PID> /F
```

### Database Connection Failed

```powershell
# Check SQL Server logs
docker-compose logs sqlserver

# Restart SQL Server
docker-compose restart sqlserver

# Wait 60+ seconds for SQL Server to fully initialize
```

### Container Won't Start

```powershell
# Check detailed logs
docker-compose logs webapi

# Rebuild without cache
docker build --no-cache -t filechecker:latest .

# Restart fresh
docker-compose down -v
docker-compose up --build
```

---

## Database Commands

### Connect to SQL Server

```powershell
# Using sqlcmd
sqlcmd -S localhost,1433 -U sa -P "SecureP@ssw0rd2024" `
  -Q "SELECT @@VERSION"
```

### Apply Migrations

```powershell
# Update database to latest migration
docker-compose exec webapi dotnet ef database update

# Create new migration
docker-compose exec webapi dotnet ef migrations add MigrationName

# List migrations
docker-compose exec webapi dotnet ef migrations list
```

### Reset Database

```powershell
# WARNING: Deletes all data!
docker-compose down -v
docker-compose up
```

---

## API Testing

### Via PowerShell (Get Records)

```powershell
curl -X GET "http://localhost:8080/api/file/record" `
  -Headers @{"X-API-KEY" = "Your-Super-Secret-Local-Development-Key-12345"}
```

### Via PowerShell (Upload CSV)

```powershell
curl -X POST "http://localhost:8080/api/file/upload" `
  -Headers @{"X-API-KEY" = "Your-Super-Secret-Local-Development-Key-12345"} `
  -Form @{File = @("C:\path\to\file.csv")}
```

### Via Swagger UI

1. Open: `http://localhost:8080/swagger`
2. Click on endpoint
3. Click "Try it out"
4. Click "Execute"

---

## Quick Reference Cheat Sheet

```powershell
# Build image
docker build -t filechecker:latest .

# Start services (foreground)
docker-compose up

# Start services (background)
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f

# Check status
docker-compose ps

# Rebuild and restart
docker-compose up --build

# Remove everything (including database)
docker-compose down -v

# Execute command in container
docker-compose exec webapi <command>
```
