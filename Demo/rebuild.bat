docker build -t asia.gcr.io/stellar-38931/testnft:master .
docker push asia.gcr.io/stellar-38931/testnft:master

cd k8s
kubectl delete -f 4-Deploy
kubectl create -f 4-Deploy

pause