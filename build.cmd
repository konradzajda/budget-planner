docker build . -t tivix-budget-planner-api:local
docker stop budget-planner
docker rm budget-planner
START docker run -p 8080:80 --name budget-planner tivix-budget-planner-api:local &
