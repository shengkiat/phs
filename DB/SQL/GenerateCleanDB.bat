cls
break> phs_CleanDB.sql 
@ECHO --------------------------Table Structure----------------- >> phs_CleanDB.sql 
type 1_Setup\TableStructure.sql >> phs_CleanDB.sql
@ECHO --------------------------Data--------------------- >> phs_CleanDB.sql
type 2_Data\Data.sql >> phs_CleanDB.sql
type 2_Data\AddressData.sql >> phs_CleanDB.sql

@ECHO DONE !

pause
