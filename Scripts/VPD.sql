ALTER SESSION SET CONTAINER= PDBQLDH; 
ALTER SESSION SET CURRENT_SCHEMA = pdb_admin;

--Câu 3: Em hãy ép thỏa các chính sách bảo mật trên quan hệ SINHVIEN dùng cơ chế VPD
--theo mô tả bên dưới và cài đặt giao diện cho những người dùng liên quan:

CREATE OR REPLACE FUNCTION pdb_admin.student_policy_function (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
    v_user_id VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_vaitro VARCHAR2(30);
BEGIN
    -- Kiểm tra vai trò từ QLDH_NHANVIEN
    BEGIN
        SELECT VAITRO INTO v_vaitro
        FROM pdb_admin.QLDH_NHANVIEN
        WHERE MANV = v_user_id
        AND ROWNUM = 1;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_vaitro := NULL;
    END;

    -- Sinh viên: chỉ thấy và cập nhật DCHI, DT của chính mình
    IF v_vaitro IS NULL THEN
        RETURN 'UPPER(MASV) = ''' || UPPER(v_user_id) || '''';
    -- NV PCTSV hoặc NV PĐT: truy cập toàn bộ bảng
    ELSIF v_vaitro IN ('NV CTSV', 'NV PĐT') THEN
        RETURN '1=1';
    -- Giảng viên: chỉ thấy sinh viên
    
    ELSIF v_vaitro = 'GV' THEN
        RETURN 'KHOA IN (SELECT MADV FROM QLDH_NHANVIEN WHERE MANV = ''' || 
               v_user_id || ''' AND VAITRO = ''GV'')';
    END IF;

    -- Mặc định: chặn truy cập
    RETURN '1=0';
EXCEPTION
    WHEN OTHERS THEN
        RETURN '1=0';
END;
/

-- Xóa các chính sách VPD cũ để tránh xung đột
B







FOR pol IN (SELECT POLICY_NAME FROM DBA_POLICIES WHERE OBJECT_NAME = 'QLDH_SINHVIEN' AND OBJECT_OWNER = 'PDB_ADMIN') LOOP
        DBMS_RLS.DROP_POLICY(
            object_schema => 'PDB_ADMIN',
            object_name   => 'QLDH_SINHVIEN',
            policy_name   => pol.POLICY_NAME
        );
    END LOOP;
END;
/



-- Áp dụng chính sách VPD mới
BEGIN
    DBMS_RLS.ADD_POLICY(
        object_schema   => 'PDB_ADMIN',
        object_name     => 'QLDH_SINHVIEN',
        policy_name     => 'student_policy',
        function_schema => 'PDB_ADMIN',
        policy_function => 'student_policy_function',
        statement_types => 'SELECT, UPDATE, DELETE',
        update_check    => TRUE,
        sec_relevant_cols => 'DCHI,DT'
    );
END;
/


CREATE OR REPLACE TRIGGER pdb_admin.trg_restrict_pctsv_update
BEFORE UPDATE OF TINHTRANG ON PDB_ADMIN.QLDH_SINHVIEN
FOR EACH ROW
DECLARE
    v_vaitro VARCHAR2(30);
BEGIN
    -- Chỉ kiểm tra nếu TINHTRANG thay đổi
            -- Kiểm tra vai trò người dùng
            BEGIN
                SELECT VAITRO INTO v_vaitro
                FROM PDB_ADMIN.QLDH_NHANVIEN
                WHERE MANV = UPPER(SYS_CONTEXT('USERENV', 'SESSION_USER'))
                AND ROWNUM = 1;
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    -- Nếu không tìm thấy, cho phép cập nhật (giả định không phải NV PCTSV)
                    RETURN;
            END;
            
            -- Ngăn NV PCTSV cập nhật TINHTRANG
            IF v_vaitro = 'NV CTSV' THEN
                RAISE_APPLICATION_ERROR(-20001, 'NV CTSV khong duoc cap nhat cot TINHTRANG.');
            END IF;
END;
/

---- Tạo vai trò nếu chưa tồn tại
--BEGIN
--    EXECUTE IMMEDIATE 'CREATE ROLE SV';
--EXCEPTION
--    WHEN OTHERS THEN
--        IF SQLCODE = -01921 THEN NULL; -- Vai trò đã tồn tại
--        ELSE RAISE;
--        END IF;
--END;
--/
--BEGIN
--    EXECUTE IMMEDIATE 'CREATE ROLE NV_CTSV';
--EXCEPTION
--    WHEN OTHERS THEN
--        IF SQLCODE = -01921 THEN NULL;
--        ELSE RAISE;
--        END IF;
--END;
--/
--BEGIN
--    EXECUTE IMMEDIATE 'CREATE ROLE NV_PDT';
--EXCEPTION
--    WHEN OTHERS THEN
--        IF SQLCODE = -01921 THEN NULL;
--        ELSE RAISE;
--        END IF;
--END;
--/
--BEGIN
--    EXECUTE IMMEDIATE 'CREATE ROLE NV_GV';
--EXCEPTION
--    WHEN OTHERS THEN
--        IF SQLCODE = -01921 THEN NULL;
--        ELSE RAISE;
--        END IF;
--END;
--/

-----------------------------------------------------------------------------------
BEGIN
    DBMS_RLS.DROP_POLICY(
        object_schema => 'PDB_ADMIN',
        object_name   => 'QLDH_DANGKY',
        policy_name   => 'register_policy'  -- hoặc thay bằng tên đúng nếu khác
    );
END;
/

SELECT *
FROM DBA_POLICIES
WHERE OBJECT_OWNER = 'PDB_ADMIN'
  AND OBJECT_NAME = 'QLDH_DANGKY';
DROP TRIGGER PDB_ADMIN.TRG_RESTRICT_DANGKY;
DROP TRIGGER PDB_ADMIN.TRG_RESTRICT_DANGKY;


--Câu 4: Em hãy ép thỏa các chính sách bảo mật trên quan hệ ĐANGKY dùng cơ chế VPD
--theo mô tả bên dưới và cài đặt giao diện cho những người dùng liên quan:

CREATE OR REPLACE FUNCTION pdb_admin.register_policy_function(
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
    v_user_id VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_vaitro VARCHAR2(30);
BEGIN
    -- Kiểm tra vai trò người dùng
    BEGIN
        SELECT VAITRO INTO v_vaitro
        FROM PDB_ADMIN.QLDH_NHANVIEN
        WHERE MANV = v_user_id;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_vaitro := NULL; -- Giả định là sinh viên
    END;
    
    -- Sinh viên: Chỉ thấy bản ghi của mình
    IF v_vaitro IS NULL THEN
        RETURN 'MASV = ''' || v_user_id || '''';
    -- NV PĐT, NV PKT: Thấy tất cả bản ghi
    ELSIF v_vaitro IN ('NV PĐT', 'NV PKT') THEN
        RETURN '1=1';
    -- Giảng viên: Chỉ thấy bản ghi liên quan đến môn học họ dạy
    ELSIF v_vaitro = 'GV' THEN
        RETURN 'MAMH IN (SELECT MAMH FROM PDB_ADMIN.QLDH_MONHOC WHERE MAGV = ''' || v_user_id || ''')';
    END IF;

    -- Mặc định: Chặn truy cập
    RETURN '1=0';
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error in register_policy_function: ' || SQLERRM);
        RETURN '1=0';
END;
/

BEGIN
    FOR pol IN (SELECT POLICY_NAME FROM DBA_POLICIES WHERE OBJECT_NAME = 'QLDH_DANGKY' AND OBJECT_OWNER = 'PDB_ADMIN') LOOP
        DBMS_RLS.DROP_POLICY(
            object_schema => 'PDB_ADMIN',
            object_name   => 'QLDH_DANGKY',
            policy_name   => pol.POLICY_NAME
        );
    END LOOP;
    
    DBMS_RLS.ADD_POLICY(
        object_schema   => 'PDB_ADMIN',
        object_name     => 'QLDH_DANGKY',
        policy_name     => 'register_policy',
        function_schema => 'PDB_ADMIN',
        policy_function => 'register_policy_function',
        statement_types => 'SELECT, INSERT, UPDATE, DELETE',
        update_check    => TRUE
    );
END;
/

CREATE OR REPLACE TRIGGER pdb_admin.trg_restrict_dangky
BEFORE INSERT OR UPDATE OR DELETE ON PDB_ADMIN.QLDH_DANGKY
FOR EACH ROW
DECLARE
    v_vaitro VARCHAR2(30);
    v_ngaybatdau DATE;
BEGIN
    -- Kiểm tra vai trò người dùng
    BEGIN
        SELECT VAITRO INTO v_vaitro
        FROM PDB_ADMIN.QLDH_NHANVIEN
        WHERE MANV = SYS_CONTEXT('USERENV', 'SESSION_USER');
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_vaitro := NULL; -- Giả định là sinh viên
    END;
    
    -- Lấy NGAYBATDAU từ QLDH_MONHOC
    BEGIN
        IF INSERTING OR UPDATING THEN
            SELECT NGAYBATDAU INTO v_ngaybatdau
            FROM PDB_ADMIN.QLDH_MONHOC
            WHERE MAMH = :NEW.MAMH;
        ELSIF DELETING THEN
            SELECT NGAYBATDAU INTO v_ngaybatdau
            FROM PDB_ADMIN.QLDH_MONHOC
            WHERE MAMH = :OLD.MAMH;
        END IF;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20002, 'MAMH không tồn tại trong QLDH_MONHOC.');
    END;
    
    -- Sinh viên
    IF v_vaitro IS NULL THEN
        -- Kiểm tra thời gian (14 ngày đầu học kỳ)
        IF SYSDATE < v_ngaybatdau OR SYSDATE > v_ngaybatdau + 14 THEN
            RAISE_APPLICATION_ERROR(-20003, 'Sinh viên chỉ được thao tác trong 14 ngày đầu học kỳ.');
        END IF;

        
        -- Kiểm tra MASV khớp với SESSION_USER
        IF INSERTING OR UPDATING THEN
            IF :NEW.MASV != SYS_CONTEXT('USERENV', 'SESSION_USER') THEN
                RAISE_APPLICATION_ERROR(-20004, 'Sinh viên chỉ được thao tác trên bản ghi của mình.');
            END IF;
        ELSIF DELETING THEN
            IF :OLD.MASV != SYS_CONTEXT('USERENV', 'SESSION_USER') THEN
                RAISE_APPLICATION_ERROR(-20004, 'Sinh viên chỉ được thao tác trên bản ghi của mình.');
            END IF;
        END IF;
    
    -- NV PĐT
    ELSIF v_vaitro = 'NV PĐT' THEN
        -- Kiểm tra thời gian (14 ngày đầu học kỳ)
        IF SYSDATE < v_ngaybatdau OR SYSDATE > v_ngaybatdau + 14 THEN
            RAISE_APPLICATION_ERROR(-20005, 'NV PĐT chỉ được thao tác trong 14 ngày đầu học kỳ.');
        END IF;
        -- Đảm bảo điểm số là NULL
        
    
    -- NV PKT
    ELSIF v_vaitro = 'NV PKT' THEN
        -- Chỉ cho phép cập nhật điểm số
        IF INSERTING OR DELETING THEN
            RAISE_APPLICATION_ERROR(-20006, 'NV PKT không được thêm hoặc xóa bản ghi.');
        END IF;
        IF UPDATING THEN
            -- Cho phép cập nhật DIEMTH, DIEMQT, DIEMCK, DIEMTK
            -- Ngăn cập nhật các cột khác
            IF :NEW.MASV != :OLD.MASV OR :NEW.MAMH != :OLD.MAMH OR :NEW.NGAYDK != :OLD.NGAYDK THEN
                RAISE_APPLICATION_ERROR(-20007, 'NV PKT chỉ được cập nhật các cột điểm số.');
            END IF;
        END IF;
    
    -- Giảng viên (GV)
    ELSIF v_vaitro = 'GV' THEN
        -- Giảng viên không được INSERT, UPDATE, DELETE (chỉ xem qua VPD)
        RAISE_APPLICATION_ERROR(-20008, 'Giảng viên không được thêm, sửa, xóa bản ghi đăng ký.');
    END IF;
END;
/