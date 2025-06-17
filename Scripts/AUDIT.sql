ALTER SESSION SET CONTAINER= PDBQLDH;
SELECT VALUE FROM V$OPTION WHERE PARAMETER = 'Unified Auditing';
--GRANT EXECUTE ON DBMS_FGA TO ADMIN_ROLE;
SELECT DB_USER, OBJECT_NAME, SQL_TEXT
FROM DBA_FGA_AUDIT_TRAIL
WHERE OBJECT_NAME = 'QLDH_NHANVIEN' AND ACTION_NAME='SELECT';

SELECT 
  DB_USER,              -- Người dùng thực hiện hành động
  OBJECT_NAME,          -- Tên bảng bị truy cập (QLDH_NHANVIEN)
  POLICY_NAME,          -- Tên chính sách audit kích hoạt
  STATEMENT_TYPE,       -- Loại thao tác: SELECT, UPDATE, DELETE, ...
  SQL_TEXT,             -- Câu lệnh SQL thực tế
  EXTENDED_TIMESTAMP    -- Thời gian chính xác khi hành động xảy ra
FROM 
  DBA_FGA_AUDIT_TRAIL
WHERE 
  OBJECT_NAME = 'QLDH_NHANVIEN' AND STATEMENT_TYPE = 'SELECT' 
ORDER BY 
  EXTENDED_TIMESTAMP DESC;

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




