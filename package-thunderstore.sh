#!/bin/bash
cp ./README.md ./Thunderstore/
cp ./manifest.json ./Thunderstore/
cd ./Thunderstore/
zip -r KeysLockDoorsPkg.zip .
ls
cp KeysLockDoorsPkg.zip /mnt/c/Users/oblivion/Downloads/
cd ../
