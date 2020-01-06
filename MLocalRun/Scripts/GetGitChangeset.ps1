Param(
$USERNAME,
$PASSWORD,
$STYLELABSDIR,
$VERSION,
$RepoExist,
$CHANGESET
)
IF($RepoExist -eq "1") 
{
Write-Host "Geting git changeset start";
cd $STYLELABSDIR #Windows path 
cd stylelabs.m
git checkout $CHANGESET -f

git pull
return 2
exit;
}