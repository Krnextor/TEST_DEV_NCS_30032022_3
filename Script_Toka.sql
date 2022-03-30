create DATABASE TOKA
GO

USE TOKA
GO


CREATE TABLE Tb_PersonasFisicas
(
    IdPersonaFisica INT IDENTITY,
    FechaRegistro DATETIME,
    FechaActualizacion DATETIME,
    Nombre VARCHAR(50),
    ApellidoPaterno VARCHAR(50),
    ApellidoMaterno VARCHAR(50),
    RFC VARCHAR(13),
    FechaNacimiento DATE,
    UsuarioAgrega INT,
    Activo BIT
);

ALTER TABLE Tb_PersonasFisicas
ADD CONSTRAINT [PK_Tb_PersonasFisicas]
    PRIMARY KEY (IdPersonaFisica);

ALTER TABLE Tb_PersonasFisicas
ADD CONSTRAINT [DF_Tb_PersonasFisicas_FechaRegistro]
    DEFAULT (GETDATE()) FOR FechaRegistro;

ALTER TABLE Tb_PersonasFisicas
ADD CONSTRAINT [DF_Tb_PersonasFisicas_Activo]
    DEFAULT (1) FOR Activo;
GO
-- ===================================================================
CREATE PROCEDURE dbo.sp_AgregarPersonaFisica
(
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @RFC VARCHAR(13),
    @FechaNacimiento DATE,
    @UsuarioAgrega INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF LEN(@RFC) != 13
        BEGIN
            SELECT @ERROR = 'El RFC no es válido';
            THROW 50000, @ERROR, 1;
        END;
        IF EXISTS
        (
            SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE RFC = @RFC
                  AND Activo = 1
        )
        BEGIN
            SELECT @ERROR = 'El RFC ya existe en el sistema';
            THROW 50000, @ERROR, 1;
        END;

        INSERT INTO dbo.Tb_PersonasFisicas
        (
            FechaRegistro,
            FechaActualizacion,
            Nombre,
            ApellidoPaterno,
            ApellidoMaterno,
            RFC,
            FechaNacimiento,
            UsuarioAgrega,
            Activo
        )
        VALUES
        (   GETDATE(),        -- FechaRegistro - datetime
            NULL,             -- FechaActualizacion - datetime
            @Nombre,          -- Nombre - varchar(50)
            @ApellidoPaterno, -- ApellidoPaterno - varchar(50)
            @ApellidoMaterno, -- ApellidoMaterno - varchar(50)
            @RFC,             -- RFC - varchar(13)
            @FechaNacimiento, -- FechaNacimiento - date
            @UsuarioAgrega,   -- UsuarioAgrega - int
            1                 -- Activo - bit
            );

        SELECT @ID = SCOPE_IDENTITY();
        SELECT @ID AS ERROR,
               'Registro exitoso' AS MENSAJEERROR;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al guardar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO

ALTER PROCEDURE dbo.sp_ActualizarPersonaFisica
(
    @IdPersonaFisica INT,
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @RFC VARCHAR(13),
    @FechaNacimiento DATE,
    @UsuarioAgrega INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF NOT EXISTS
        (
            SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE IdPersonaFisica = @IdPersonaFisica
                  AND Activo = 1
        )
        BEGIN
            SELECT @ERROR = 'La persona fisica no existe.';
            THROW 50000, @ERROR, 1;
        END;

        UPDATE dbo.Tb_PersonasFisicas
        SET Nombre = @Nombre,
            ApellidoPaterno = @ApellidoPaterno,
            ApellidoMaterno = @ApellidoMaterno,
            RFC = @RFC,
            FechaNacimiento = @FechaNacimiento,
			FechaActualizacion = GETDATE()
        WHERE IdPersonaFisica = @IdPersonaFisica;
        SELECT @IdPersonaFisica AS ERROR,
               'Registro exitoso' AS MENSAJEERROR;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO


alter PROCEDURE dbo.sp_EliminarPersonaFisica
(@IdPersonaFisica INT)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF not EXISTS
        (
            SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE IdPersonaFisica = @IdPersonaFisica
                  AND Activo = 1
        )
        BEGIN
            SELECT @ERROR = 'La persona fisica no existe.';
            THROW 50000, @ERROR, 1;
        END;

        UPDATE dbo.Tb_PersonasFisicas
        SET Activo = 0,
		FechaActualizacion = GETDATE()
        WHERE IdPersonaFisica = @IdPersonaFisica;
		SELECT @IdPersonaFisica AS ERROR,
               'Registro Eliminado' AS MENSAJEERROR;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO


CREATE PROCEDURE dbo.sp_ConsultarPersonasFisicas
AS
BEGIN
	
	SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE Activo = 1
END
GO

CREATE PROCEDURE dbo.sp_ConsultarUnaPersonaFisica
(
	@IdPersonaFisica INT
)
AS
BEGIN
    
	SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE IdPersonaFisica = @IdPersonaFisica
                  AND Activo = 1

END;
GO


CREATE TABLE dbo.Usuarios
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Usuario varchar(30),
	Contrasenia varchar(30),
	FechaRegistro DATETIME default GETDATE(),
	Activo INT DEFAULT 1
)
go

INSERT INTO dbo.Usuarios(Usuario, Contrasenia)
Values('Administrador', 'admin12345')
GO


CREATE PROCEDURE dbo.sp_loginUsuario
(
	@Usuario varchar(30) = NULL,
	@Contrasenia varchar(30) = NULL
)
AS 
BEGIN

	select TOP 1 ID, Usuario from dbo.Usuarios
	where Usuario = @Usuario and Contrasenia = @Contrasenia and Activo = 1


END
GO


exec dbo.sp_loginUsuario @Usuario  = 'Administrador', @Contrasenia = 'admin12345'

