ALTER SESSION SET CONTAINER= PDBQLDH; 
ALTER SESSION SET CURRENT_SCHEMA = pdb_admin;

CREATE ROLE NV_NVCB;        -- Nhân viên cơ bản
CREATE ROLE NV_GV;          -- Giảng viên
CREATE ROLE NV_PDT;         -- Nhân viên Phòng Đào tạo
CREATE ROLE NV_PKT;         -- Nhân viên Phòng Khảo thí
CREATE ROLE NV_TCHC;        -- Nhân viên Phòng Tổ chức hành chính
CREATE ROLE NV_CTSV;        -- Nhân viên Phòng Công tác sinh viên
CREATE ROLE NV_TRGDV;       -- Trưởng đơn vị
CREATE ROLE SV;
-- Tạo ROLE mới
CREATE ROLE LOGIN_ROLE;

-- Gán quyền SELECT trên các bảng cho ROLE
GRANT SELECT ON pdb_admin.QLDH_ADMIN TO LOGIN_ROLE;
GRANT SELECT ON pdb_admin.QLDH_NHANVIEN TO LOGIN_ROLE;
GRANT SELECT ON pdb_admin.QLDH_SINHVIEN TO LOGIN_ROLE;
GRANT RESTRICTED SESSION TO LOGIN_ROLE;
GRANT SELECT ON pdb_admin.QLDH_DONVI TO LOGIN_ROLE;
GRANT UPDATE (DT) ON pdb_admin.QLDH_NHANVIEN TO LOGIN_ROLE;

---------------------
-- Tạo procedure gán role cho user
CREATE OR REPLACE PROCEDURE GRANT_ROLE_TO_USER (
    p_username IN VARCHAR2,
    p_roles IN VARCHAR2     -- Danh sách các role đã được chọn
)
IS
    v_role VARCHAR2(100);
    v_roles DBMS_UTILITY.lname_array;
    v_count INTEGER;
BEGIN
    -- Tách chuỗi p_roles thành mảng
    DBMS_UTILITY.COMMA_TO_TABLE(p_roles, v_count, v_roles);
    FOR i IN 1 .. v_count LOOP
        EXECUTE IMMEDIATE 'GRANT ' || v_roles(i) || ' TO ' || p_username;
    END LOOP;
END;
/

-- Gán quyền cho Role
CREATE OR REPLACE PROCEDURE GRANT_PRIVS_TO_ROLE (
    v_role IN VARCHAR2,
    v_table IN VARCHAR2,
    v_privs IN VARCHAR2
)
IS
    v_sql VARCHAR2(1000);
BEGIN
    v_sql := 'GRANT ' || v_privs || ' ON ' || v_table || ' TO ' || v_role;
    
    -- Thực thi
    EXECUTE IMMEDIATE v_sql;
EXCEPTION
    WHEN OTHERS THEN
        NULL;
END;
/

-- Thu hồi quyền của role
CREATE OR REPLACE PROCEDURE REVOKE_PRIVS_FROM_ROLE (
    v_role IN VARCHAR2,
    v_table IN VARCHAR2,
    v_privs IN VARCHAR2
)
IS
    v_sql VARCHAR2(1000);
BEGIN
    v_sql := 'REVOKE ' || v_privs || ' ON ' || v_table || ' FROM ' || v_role;
    
    EXECUTE IMMEDIATE v_sql;
    
EXCEPTION
    WHEN OTHERS THEN
        NULL;
END;
/

-- Cấp quyền cho user
CREATE OR REPLACE PROCEDURE GRANT_PRIV_TO_USER (
    v_user IN VARCHAR2,
    v_table IN VARCHAR2,
    v_privs IN VARCHAR2,
    v_grant_option IN BOOLEAN
)
IS
    v_sql VARCHAR2(1000);
BEGIN
    v_sql := 'GRANT ' || v_privs || ' ON ' || v_table || ' TO ' || v_user;
    
    -- Kiểm tra WITH GRANT OPTION
    IF v_grant_option THEN
        v_sql := v_sql || ' WITH GRANT OPTION';
    END IF;
    
    EXECUTE IMMEDIATE v_sql;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20003, 'Lỗi khi cấp quyền cho user: ' || SQLERRM);
END;
/

-- Thu hồi quyền user
CREATE OR REPLACE PROCEDURE REVOKE_PRIVS_FROM_USER (
    v_username IN VARCHAR2,
    v_table IN VARCHAR2,
    v_privs IN VARCHAR2
)
IS
    v_sql VARCHAR2(1000);
BEGIN
    v_sql := 'REVOKE ' || v_privs || ' ON ' || v_table || ' FROM ' || v_username;
    
    EXECUTE IMMEDIATE v_sql;
    
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20004, 'Lỗi khi thu hồi quyền: ' || SQLERRM);
END;
/

-- VÌ ĐÃ GÁN QUYỀN DBA
--GRANT EXECUTE ON GRANT_PRIVS_TO_ROLE TO pdb_admin;
--GRANT EXECUTE ON REVOKE_PRIVS_FROM_ROLE TO pdb_admin;
--GRANT EXECUTE ON GRANT_PRIV_TO_USER TO pdb_admin;
--GRANT EXECUTE ON REVOKE_PRIVS_FROM_USER TO pdb_admin;

--------------------------------------------------------------------------------
/*-- Test procedure
CREATE ROLE TEST_ROLE_01;
CREATE ROLE TEST_ROLE_02;
CREATE ROLE TEST_ROLE_03;
CREATE ROLE FIX_ROLE_BUGS;

DROP ROLE TEST_ROLE_03;

CREATE USER TEST_USER_01 IDENTIFIED BY TEST;
CREATE USER TEST_USER_02 IDENTIFIED BY TEST;
CREATE USER TEST_USER_03 IDENTIFIED BY TEST;

GRANT SV TO TEST_USER_01;
GRANT TEST_ROLE_03 TO TEST_USER_03, TEST_USER_01;
GRANT TEST_ROLE_02 TO TEST_USER_02;

BEGIN
    GRANT_ROLE_TO_USER('TEST_USER_01', 'TEST_ROLE_01,TEST_ROLE_02');
    GRANT_ROLE_TO_USER('TEST_USER_02', 'TEST_ROLE_02,TEST_ROLE_03');
END;
/

/*
-- Kiểm tra
SELECT * FROM DBA_ROLE_PRIVS WHERE GRANTEE = 'TEST_USER_01';
SELECT * FROM DBA_ROLE_PRIVS WHERE GRANTEE = 'TEST_USER_01';


-- Tạo bảng tạm
CREATE TABLE SAMPLE_TABLE (
    ID NUMBER,
    NAME VARCHAR2(100)
);

BEGIN
    -- Cấp quyền SELECT và INSERT cho role
    GRANT_PRIVS_TO_ROLE('TEST_ROLE_01', 'SAMPLE_TABLE', 'SELECT, INSERT');
    -- Cấp quyền DELETE có WITH GRANT OPTION
    GRANT_PRIVS_TO_ROLE('TEST_ROLE_02', 'SAMPLE_TABLE', 'DELETE');
END;
/

-- Thu hồi quyền
BEGIN
    -- Thu hồi quyền DELETE khỏi TEST_ROLE_02
    REVOKE_PRIVS_FROM_ROLE('TEST_ROLE_01', 'SAMPLE_TABLE', 'SELECT, INSERT');
END;
/

-- Cấp quyền cho user
BEGIN
    GRANT_PRIV_TO_USER('TEST_USER_03', 'SAMPLE_TABLE', 'SELECT, INSERT, DELETE, UPDATE', TRUE);
    GRANT_PRIV_TO_USER('TEST_USER_01', 'SAMPLE_TABLE', 'SELECT, INSERT, DELETE', FALSE);
END;
/

BEGIN
    REVOKE_PRIVS_FROM_USER('TEST_USER_01', 'SAMPLE_TABLE', 'DELETE');
END;
/

SELECT * FROM DBA_TAB_PRIVS WHERE GRANTEE IN ('TEST_ROLE_01', 'TEST_ROLE_02', 'TEST_ROLE_03');
SELECT * FROM DBA_TAB_PRIVS WHERE GRANTEE IN ('TEST_USER_01', 'TEST_USER_02', 'TEST_USER_03');
*/

------------------------------------------------------------------------------------------------
--CÂU 1:
--Người dùng có VAITRO là “NVCB” có quyền xem dòng dữ liệu của chính mình trong
--quan hệ NHANVIEN, có thể chỉnh sửa số điện thoại (ĐT) của chính mình (nếu số điện
--thoại có thay đổi). Tất cả nhân viên thuộc các vai trò còn lại đều có quyền của vai trò “NVCB”.

CREATE OR REPLACE VIEW QLDH_VIEW_EMP_INFO AS
SELECT * FROM QLDH_NHANVIEN 
WHERE MANV = SYS_CONTEXT('USERENV','SESSION_USER');




GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_GV;
GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_PDT;
GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_PKT;
GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_TCHC;
GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_CTSV;
GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_TRGDV;
GRANT SELECT, UPDATE(DT) ON QLDH_VIEW_EMP_INFO TO NV_NVCB; 


--Người dùng có VAITRO là “TRGĐV” có quyền xem các dòng dữ liệu liên quan đến
--các nhân viên thuộc đơn vị mình làm trưởng, trừ các thuộc tính LUONG và PHUCAP.
CREATE OR REPLACE VIEW QLDH_VIEW_MANAGER_EMPS AS
SELECT e.MANV, e.HOTEN, e.PHAI, e.NGSINH, e.DT, e.VAITRO, e.MADV
FROM pdb_admin.QLDH_NHANVIEN m 
JOIN pdb_admin.QLDH_DONVI u ON m.MANV = u.TRGDV 
JOIN pdb_admin.QLDH_NHANVIEN e ON u.MADV = e.MADV
WHERE m.MANV = SYS_CONTEXT('USERENV', 'SESSION_USER');

GRANT SELECT ON QLDH_VIEW_MANAGER_EMPS TO NV_TRGDV;

--Người dùng có VAITRO là “NV TCHC” có quyền xem, thêm, cập nhật, xóa trên quan hệ NHANVIEN.
GRANT SELECT,INSERT,UPDATE,DELETE ON pdb_admin.QLDH_NHANVIEN TO NV_TCHC;


-------------------------------------------------------------------------------------------

--CÂU 2:
--Người dùng có vai trò “GV” được quyền xem các dòng phân công giảng dạy liên quan
--đến chính giảng viên đó.
CREATE OR REPLACE VIEW QLDH_VIEW_COURSE_INFO_GV AS
SELECT *
FROM pdb_admin.QLDH_MONHOC
WHERE MAGV = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT ON QLDH_VIEW_COURSE_INFO_GV to NV_GV;

--Người dùng có vai trò “NV PĐT” có quyền xem, thêm, cập nhật, xóa dòng trong bảng
--MOMON liên quan đến học kỳ hiện tại của năm học đang diễn ra.
CREATE OR REPLACE VIEW QLDH_VIEW_COURSE_INFO_NVPDT AS
SELECT * 
FROM pdb_admin.QLDH_MONHOC
WHERE HK = (SELECT CASE 
                   WHEN EXTRACT(MONTH FROM SYSDATE) IN (10, 11, 12, 1) THEN 1
                   WHEN EXTRACT(MONTH FROM SYSDATE) IN (2, 3, 4, 5) THEN 2
                   WHEN EXTRACT(MONTH FROM SYSDATE) IN (7, 8, 9) THEN 3
                   END
            FROM dual)
AND NAM = EXTRACT(YEAR FROM SYSDATE);

-- Grant permissions to NV_PDT role
GRANT SELECT ON QLDH_VIEW_COURSE_INFO_NVPDT TO NV_PDT

--Người dùng có vai trò “TRGĐV” có quyền xem các dòng phân công giảng dạy của
--các giảng viên thuộc đơn vị mình làm trưởng.
CREATE OR REPLACE VIEW QLDH_VIEW_COURSE_INFO_TRGDV  AS
SELECT s.MAMH, s.MAHP, s.MAGV,s.HK,s.NAM
FROM pdb_admin.QLDH_MONHOC s 
JOIN pdb_admin.QLDH_NHANVIEN e ON s.MAGV = e.MANV
JOIN pdb_admin.QLDH_DONVI u ON e.MADV = u.MADV
JOIN pdb_admin.QLDH_NHANVIEN m ON m.MANV = u.TRGDV
WHERE m.MANV  = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT ON QLDH_VIEW_COURSE_INFO_TRGDV TO NV_TRGDV;

--Sinh viên có quyền xem các dòng dữ liệu trong quan hệ MOMON liên quan các dòng
--mở các học phần thuộc quyền phụ trách chuyên môn bởi Khoa mà sinh viên đang theo
--học.

CREATE OR REPLACE VIEW QLDH_VIEW_COURSE_INFO_SV AS
SELECT sub.MAMH, m.TENHP, m.SOTC, nv.HOTEN, sub.HK,sub.NAM
FROM pdb_admin.QLDH_MONHOC sub
JOIN pdb_admin.QLDH_HOCPHAN m ON sub.MAHP = m.MAHP
JOIN pdb_admin.QLDH_SINHVIEN s ON s.KHOA = m.MADV
JOIN pdb_admin.QLDH_NHANVIEN nv ON sub.MAGV = nv.MANV
WHERE s.MASV = SYS_CONTEXT('USERENV','SESSION_USER');

CREATE OR REPLACE VIEW QLDH_VIEW_REGISTERED_INFO_SV AS
SELECT d.MAMH, m.TENHP, m.SOTC, nv.HOTEN AS GIANGVIEN, mh.HK, mh.NAM
FROM pdb_admin.QLDH_DANGKY d
JOIN pdb_admin.QLDH_MONHOC mh ON d.MAMH = mh.MAMH
JOIN pdb_admin.QLDH_HOCPHAN m ON mh.MAHP = m.MAHP
JOIN pdb_admin.QLDH_NHANVIEN nv ON mh.MAGV = nv.MANV
WHERE d.MASV = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT, UPDATE, DELETE, INSERT ON QLDH_VIEW_COURSE_INFO_SV TO SV;
GRANT SELECT, UPDATE, DELETE, INSERT ON QLDH_VIEW_REGISTERED_INFO_SV TO SV;
GRANT SELECT, UPDATE, DELETE, INSERT ON QLDH_DANGKY TO SV;