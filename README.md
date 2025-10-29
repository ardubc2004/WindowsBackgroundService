To create the service (in cmd):
sc create "ArtemService" binPath="(path to the service executable) --k --t"
example:
sc create "ArtemService" binPath="C:\Users\dubch\Documents\VS Publish Dir\MyService\ArtemService4.exe --k --t"
