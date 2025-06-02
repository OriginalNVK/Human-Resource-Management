ALTER SESSION SET CONTAINER= PDBQLDH;
SELECT VALUE FROM V$OPTION WHERE PARAMETER = 'Unified Auditing';
GRANT EXECUTE ON DBMS_FGA TO ADMIN_ROLE;

BEGIN
  DBMS_FGA.ADD_POLICY(
    object_schema   => 'PDB_ADMIN',
    object_name     => 'QLDH_NHANVIEN',
    policy_name     => 'AUDIT_QLDH_NHANVIEN',
    audit_condition =>  'MANV <> SYS_CONTEXT(''USERENV'', ''SESSION_USER'')',
    audit_column    => 'LUONG, PHUCAP',
    statement_types => 'SELECT, UPDATE',
    enable          => TRUE
);
END;
/

BEGIN
  DBMS_FGA.ADD_POLICY(
    object_schema   => 'PDB_ADMIN',
    object_name     => 'QLDH_DANGKY',
    policy_name     => 'AUDIT_QLDH_DANGKY',
    audit_column    => 'DIEMTH,DIEMCK, DIEMTK, DIEMQT',
    statement_types => 'UPDATE,INSERT,DELETE',
    enable          => TRUE
  );
END;




