Param($KeyIndex, $PathToRepo, $PathToElasticSearch)
$JsonObj = Get-Content $PathToRepo/src/Configuration/m.defaults.json | ConvertFrom-Json
Write-Host $KeyIndex


$JsonObj.redis.db=[int]$KeyIndex
$dataTOWrite = $JsonObj | ConvertTo-Json -depth 32
Set-Content $PathToRepo/src/Configuration/m.defaults.json  $dataToWrite
#Elastic Search

IF(Test-Path -Path $JsonObj.graphserver.snapshotPath)
{
Remove-Item $JsonObj.graphserver.snapshotPath -Recurse -Force 
}
Test-NetConnection localhost -Port 9200
IF(!$_.TcpTestSucceeded )
{
Start-Process $PathToElasticSearch/bin/elasticsearch

do {
  Write-Host "Waiting for Elasticsearch service to bootstrap..."
  sleep 1
} until(Test-NetConnection localhost -Port 9200 | ? { $_.TcpTestSucceeded } )

}

 Write-Host "Cleaning up elastic search" 
 Invoke-WebRequest -Method DELETE  -URI http://localhost:9200/_all
 return 1;