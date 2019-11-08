Param(
$USERNAME,
$PASSWORD,
$STYLELABSDIR,
$VERSION,
$RepoExist
)
Write-Host "Geting git repo start";
cd $STYLELABSDIR #Windows path 
IF($RepoExist -eq "1") 
{
Write-Host "Getting git repo " -ForegroundColor White
git fetch "https://$USERNAME@bitbucket.org/stylelabsdev/stylelabs.m.git"
$Output = git checkout $VERSION
Write-Host "Testingl........ " $Output;
IF([string]::IsNullOrEmpty($Output))
{
return -1
exit;
}
git pull
return 1
exit;
}
ELSE
{
Write-Host "New Repo"
git clone "https://$USERNAME@bitbucket.org/stylelabsdev/stylelabs.m.git"
cd stylelabs.m
git checkout $VERSION 
IF([string]::IsNullOrEmpty($Output))
{
return -2
exit;
}
git pull
return 2
exit;
}