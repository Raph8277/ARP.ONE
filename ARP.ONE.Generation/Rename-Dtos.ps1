# Renomme les fichiers .cs du dossier .\DataTransfertObject en <NomDeClasseDto>.cs
# Ne traite que les fichiers contenant le marqueur "TEMPLATE DTO IS USED"

$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$dtoRoot = Join-Path $projectRoot "DataTransfertObject"

Write-Host "ProjectRoot : $projectRoot"
Write-Host "DtoRoot     : $dtoRoot"

if (-not (Test-Path $dtoRoot)) {
    Write-Error "Le dossier 'DataTransfertObject' est introuvable à la racine du projet."
    exit 1
}

# Regex robuste : class/record + sealed/partial dans n'importe quel ordre
$rx = [regex]'public\s+(?:(?:sealed|partial)\s+){0,2}(?:class|record)\s+([A-Za-z_][A-Za-z0-9_]*)\b'

$seen = 0
$skippedNoMarker = 0
$skippedNoMatch = 0
$skippedNotDto = 0
$skippedAlreadyOk = 0
$skippedCollision = 0
$renamed = 0
$errors = 0

Get-ChildItem -Path $dtoRoot -Filter *.cs -Recurse -File | ForEach-Object {
    $seen++
    $file = $_

    try {
        $content = Get-Content $file.FullName -Raw -ErrorAction Stop

        if ($content -notmatch 'TEMPLATE DTO IS USED') {
            $skippedNoMarker++
            return
        }

        $m = $rx.Match($content)
        if (-not $m.Success) {
            $skippedNoMatch++
            Write-Warning "Classe non détectée dans: $($file.FullName)"
            return
        }

        $typeName = $m.Groups[1].Value
        if ($typeName -notlike '*Dto') {
            $skippedNotDto++
            Write-Warning "Classe détectée mais ne finit pas par Dto ($typeName) dans: $($file.FullName)"
            return
        }

        $expectedFileName = "$typeName.cs"
        if ($file.Name -ieq $expectedFileName) {
            $skippedAlreadyOk++
            return
        }

        $targetPath = Join-Path $file.DirectoryName $expectedFileName
        if (Test-Path $targetPath) {
            $skippedCollision++
            Write-Warning "Collision: '$targetPath' existe déjà. Ignoré: '$($file.FullName)'"
            return
        }

        # Enlever ReadOnly si présent (Git/TFVC)
        if ($file.IsReadOnly) {
            $file.IsReadOnly = $false
        }

        Rename-Item -Path $file.FullName -NewName $expectedFileName -ErrorAction Stop
        $renamed++
        Write-Host "Renamed: '$($file.Name)' -> '$expectedFileName'"
    }
    catch {
        $errors++
        Write-Error "Erreur sur '$($file.FullName)': $($_.Exception.Message)"
    }
}

Write-Host ""
Write-Host "=== Résumé ==="
Write-Host "Fichiers scannés           : $seen"
Write-Host "Ignorés (sans marqueur)    : $skippedNoMarker"
Write-Host "Ignorés (classe non match) : $skippedNoMatch"
Write-Host "Ignorés (pas un *Dto)      : $skippedNotDto"
Write-Host "Ignorés (déjà OK)          : $skippedAlreadyOk"
Write-Host "Ignorés (collision)        : $skippedCollision"
Write-Host "Renommés                   : $renamed"
Write-Host "Erreurs                    : $errors"

if ($errors -gt 0) { exit 2 } else { exit 0 }
