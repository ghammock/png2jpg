:: Convert PNG images to JPEG
:: Gary Hammock, 2016
::

@echo off
cls

echo Converting Files

:: Use the png2jpg app to convert the PNG images.
for /r %%i in (*.png) do (
png2jpg.exe %%i
)

mkdir JPEGs
mkdir PNGs

move *.jpg JPEGs
move *.png PNGs

echo     Converting complete