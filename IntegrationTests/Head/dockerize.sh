#!/bin/bash
VERSION=$(git tag --sort=-version:refname | head -1)
MAJOR_VERSION=$(echo $VERSION | sed 's/v*\([0-9]*\).*$/\1/g')
MINOR_VERSION=$(echo $VERSION | sed 's/v*[0-9]*.\([0-9]*\).*$/\1/g')
PATCH_VERSION=$(echo $VERSION | sed 's/v*[0-9]*.[0-9]*.\([0-9]*\).*$/\1/g')
PRE_RELEASE_TAG=$(echo $VERSION | sed 's/v*[0-9]*.[0.9]*.[0-9]-\([a-zA-Z]*\).*$/\1/g')
BUILD_VERSION=$(echo $VERSION | sed 's/v*[0-9]*.[0.9]*.[0-9]-[a-zA-Z]*.\([0-9]*\)/\1/g')

[[ $PRE_RELEASE_TAG == $BUILD_VERSION ]] &&
    IMAGE_VERSION=$MAJOR_VERSION.$MINOR_VERSION.$PATCH_VERSION ||
    IMAGE_VERSION=$MAJOR_VERSION.$MINOR_VERSION.$PATCH_VERSION-$PRE_RELEASE_TAG.$BUILD_VERSION

echo Building version $IMAGE_VERSION

docker build -t dolittle/integrationtests-head-dotnet .
docker tag dolittle/integrationtests-head-dotnet dolittle/integrationtests-head-dotnet:$IMAGE_VERSION

if [ $PRE_RELEASE_TAG = $VERSION ]; then
    echo Pushing latest
    docker tag dolittle/integrationtests-head-dotnet dolittle/integrationtests-head-dotnet:latest
    docker push dolittle/integrationtests-head-dotnet:latest
fi

docker push dolittle/integrationtests-head-dotnet:$IMAGE_VERSION