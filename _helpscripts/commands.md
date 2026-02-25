kubectl get pods -n cinema
kubectl logs deployment/we-admin-be --all-pods=true -n wealthcom

# Run: docker compose up --build
# Stop: docker compose down

eval $(minikube docker-env)
docker-compose build
docker images



kubectl apply -f k8s/
kubectl apply -f ./k8s/
kubectl apply -f ./k8s/ -R

kubectl get pods -n cinema
kubectl get pods -l app=catalog-service,version=v2 -n cinema

kubectl delete service catalog-service -n cinema
kubectl delete deployment catalog-service -n cinema
kubectl get endpoints -n cinema

kubectl get events -n cinema --watch

for i in {1..10}; do curl -s http://cinema.local/catalog/movies/1; echo ""; done