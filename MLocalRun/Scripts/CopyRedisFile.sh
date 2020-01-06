cd ${args[0]}
redis-cli ping
IF(output -eq "PONG")
{

 bash -c "killall redis-server"
}
bash -c "rm dump.rdb -f"
BashPath = {args[1]} -replace "C:" , ""
bash -c "cp /mnt/c$BashPath ${args[0]}dump.rdb"

$output = bash  -c "cd ${PathToRedis} ; redis-server --daemonize yes"


 ; rm dump.rdb -f  ; cp /mnt/cBashPath PathToRedisdump.rdb ; cd PathToRedis ; redis-server --daemonize yes