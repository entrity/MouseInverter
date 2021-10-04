# Build
/mnt/c/windows/system32/cmd.exe /c \
'C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe' \
MouseInverter.csproj

# Check success
ret=$?
echo ret $ret

# Run
if !(($ret)); then
  bin/Debug/MouseInverter.exe
fi
