@echo off

echo "This batch should be run with the Developer Command Line from Visual Studio."
echo "Also this batch needs 7z to be in your path."
pause

echo "Building PuttyLauncher..."
msbuild Build.proj

echo "Zipping Folder..."
cd bin
7z a -tzip -mx9 -y PuttyLauncher.zip PuttyLauncher\
cd ..

echo "Done!"
pause