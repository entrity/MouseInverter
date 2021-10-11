# Build
# cd MouseInverter && \
# pwd && \

THISFILE=`readlink -f "$0"`
THISDIR=`dirname "${THISFILE}"`
PROJFILE="$THISDIR/MouseInverter/MouseInverter.csproj"
TOOLPATH='C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe'

function build () {
  rsync -r "$THISDIR" ~/win/home/Desktop
  cd ~/win/home/Desktop/MouseInverter/MouseInverter
  printf -v execpath "%q" "$TOOLPATH"
  execpath=$(<<<"$execpath" sed 's@\(\\\([ ()]\)\)@`\2@g')
  powershell.exe -Command "$execpath" "MouseInverter.csproj"
}

function run () {
  ~/win/home/Desktop/MouseInverter/MouseInverter/bin/Debug/MouseInverter.exe
}

(($#)) || build # Any args makes "build" get skipped
run
