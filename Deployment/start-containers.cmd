echo off

cd ..
echo Building API image
call docker build --no-cache -t wheelly_api .

cd Deployment
echo Starting containers
call docker-compose -p air_wheelly up -d

pause