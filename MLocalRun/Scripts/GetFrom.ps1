Param(
$USERNAME,
$PASSWORD,
$STYLELABSDIR,
$VERSION
)
Write-Host "Getting git repo " -ForegroundColor White
cd $STYLELABSDIR
git fetch
git checkout $VERSION
git pull
If (!$?)
{  Write-Host "Git Checkout failed" -ForegroundColor Red
   Write-Host "If you want to force checkout press 1 else press anything else" -ForegroundColor Cyan
  $Continue = Read-Host
  If($Continue -eq "1")
  {
    git checkout $VERSION -f
  
  }
  
}
cd "src"
Write-Host "Cleaning the project" -ForegroundColor White
$out = ./clean.bat
cd  $PSScriptRoot
Write-Host "Git Repo is set up." -ForegroundColor Green