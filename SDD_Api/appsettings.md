## Configurações ORACLE

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {

    "CHRIS_ORCL": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.0.82)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=CHRCLONE)(SERVER = DEDICATED)));User Id=chriserp;Password=chriserp;"
  },

  "TnsDB": "CLONE_NEW"
}
