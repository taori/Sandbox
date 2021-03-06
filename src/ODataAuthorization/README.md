# Purpose of project
The task of project is to find a good, generic way of authorizing against types and fields / type identifiers

# Current State
WIP

# Setup steps
## How to create the datacontext from database first

### Download sample database from
https://github.com/microsoft/sql-server-samples/releases

### Import Database using these db commands:
```
RESTORE FILELISTONLY 
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SS2008\MSSQL\Backup\AdventureWorks.bak'
```

and this:
```
IF DB_ID('AdventureWorks') IS NULL 
BEGIN
  RESTORE DATABASE [AdventureWorks]
  FILE = N'AdventureWorks_Data'
  FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SS2008\MSSQL\Backup\AdventureWorks.bak'
  WITH 
    FILE = 1, NOUNLOAD, STATS = 10,
    MOVE N'YOUR logical name of data file as shown by RESTORE FILELISTONLY command'
    TO N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SS2008\MSSQL\DATA\AdventureWorks.mdf',
    MOVE N'YOUR logical name of Log file as shown by RESTORE FILELISTONLY command'
    TO N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SS2008\MSSQL\DATA\AdventureWorks.LDF'
END
```

### Invoke Scaffolding
Follow steps of documentation here:
https://docs.microsoft.com/de-de/ef/core/get-started/aspnetcore/existing-db

e.g. 

`Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=AdventureWorks;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`

# Links used to get started

# Interesting links

https://github.com/OData/WebApi/blob/f9d10191efcb13fee7f995fa4ec2188860d8c6fd/src/Microsoft.AspNetCore.OData/Extensions/ODataRouteBuilderExtensions.cs#L450