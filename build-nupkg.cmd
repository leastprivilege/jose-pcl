tools\nuget.exe update -self

if not exist jose-pcl\bin\Release\nupkg mkdir jose-pcl\bin\Release\nupkg
if not exist jose-pcl\bin\Release\nupkg\content mkdir jose-pcl\bin\Release\nupkg\content
if not exist "jose-pcl\bin\Release\nupkg\lib\portable-net45+netcore45+wpa81+wp8" mkdir "jose-pcl\bin\Release\nupkg\lib\portable-net45+netcore45+wpa81+wp8"

copy jose-pcl\bin\Release\jose-pcl.dll "jose-pcl\bin\Release\nupkg\lib\portable-net45+netcore45+wpa81+wp8"

tools\nuget.exe pack jose-pcl.nuspec -BasePath jose-pcl\bin\Release\nupkg