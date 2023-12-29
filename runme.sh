#!/bin/bash
cp /mnt/c/Users/oblivion/code/KeysLockDoors/KeysLockDoors/bin/Debug/KeysLockDoors.* /mnt/c/Users/oblivion/Downloads/NetcodePatcher-2.4.0/plugins/
cd /mnt/c/Users/oblivion/Downloads/NetcodePatcher-2.4.0
./NetcodePatcher.dll plugins/ deps/
# copy to mod folder default game
cp plugins/KeysLockDoors.* /mnt/c/Users/oblivion/../All\ Users/../Program\ Files\ \(x86\)/Steam/steamapps/common/Lethal\ Company/BepInEx/plugins/KeysLockDoors
# copy to r2modman folder sword bunny profile
cp plugins/KeysLockDoors.* /mnt/c/Users/oblivion/AppData/Roaming/r2modmanPlus-local/LethalCompany/profiles/sword\ and\ bunny/BepInEx/plugins/KeysLockDoors 
# copy to the github repo folder
cp plugins/KeysLockDoors.* /mnt/c/Users/oblivion/code/KeysLockDoors/Thunderstore/plugins/KeysLockDoors/
7z a KeysLockDoors.zip /mnt/c/Users/oblivion/../All\ Users/../Program\ Files\ \(x86\)/Steam/steamapps/common/Lethal\ Company/BepInEx/plugins/KeysLockDoors
mv KeysLockDoors.zip /mnt/c/Users/oblivion/Downloads/
ls -alh /mnt/c/Users/oblivion/../All\ Users/../Program\ Files\ \(x86\)/Steam/steamapps/common/Lethal\ Company/BepInEx/plugins/KeysLockDoors
