
CREATE TABLE IF NOT EXISTS "ef_temp_Users" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY,
    "Created" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "IsDeleted" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "TypeUser" nvarchar(50) NOT NULL,
    "Updated" TEXT NULL
);


CREATE TABLE IF NOT EXISTS"ef_temp_ExpenseTypes" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ExpenseTypes" PRIMARY KEY,
    "Created" TEXT NOT NULL,
    "IsDeleted" INTEGER NOT NULL,
    "IsFixed" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "Updated" TEXT NULL
);


CREATE TABLE IF NOT EXISTS"ef_temp_Expenses" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Expenses" PRIMARY KEY,
    "CategoryId" TEXT NOT NULL,
    "Created" TEXT NOT NULL,
    "ExpenseTypeId" TEXT NOT NULL,
    "Installments" INTEGER NOT NULL,
    "IsDeleted" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "Updated" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "FK_Expenses_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Expenses_ExpenseTypes_ExpenseTypeId" FOREIGN KEY ("ExpenseTypeId") REFERENCES "ExpenseTypes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Expenses_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);


CREATE TABLE IF NOT EXISTS"ef_temp_ExpenseInstallments" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ExpenseInstallments" PRIMARY KEY,
    "Amount" REAL NOT NULL,
    "Created" TEXT NOT NULL,
    "DueDate" TEXT NOT NULL,
    "ExpenseId" TEXT NOT NULL,
    "InstallmentNumber" INTEGER NOT NULL,
    "IsDeleted" INTEGER NOT NULL,
    "IsPaid" INTEGER NOT NULL,
    "Updated" TEXT NULL,
    CONSTRAINT "FK_ExpenseInstallments_Expenses_ExpenseId" FOREIGN KEY ("ExpenseId") REFERENCES "Expenses" ("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS"ef_temp_Categories" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY,
    "Created" TEXT NOT NULL,
    "IsDeleted" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "Updated" TEXT NULL,
    "UserId" TEXT NULL,
    CONSTRAINT "FK_Categories_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id")
);



PRAGMA foreign_keys = 0;



ALTER TABLE "ef_temp_Users" RENAME TO "Users";


ALTER TABLE "ef_temp_ExpenseTypes" RENAME TO "ExpenseTypes";



ALTER TABLE "ef_temp_Expenses" RENAME TO "Expenses";



ALTER TABLE "ef_temp_ExpenseInstallments" RENAME TO "ExpenseInstallments";



ALTER TABLE "ef_temp_Categories" RENAME TO "Categories";

PRAGMA foreign_keys = 1;


CREATE INDEX "IX_Expenses_CategoryId" ON "Expenses" ("CategoryId");

CREATE INDEX "IX_Expenses_ExpenseTypeId" ON "Expenses" ("ExpenseTypeId");

CREATE INDEX "IX_Expenses_UserId" ON "Expenses" ("UserId");

CREATE INDEX "IX_ExpenseInstallments_ExpenseId" ON "ExpenseInstallments" ("ExpenseId");

CREATE INDEX "IX_Categories_UserId" ON "Categories" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241227021129_ChangeIdToGuid', '7.0.8');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241227025042_InitialCreate', '7.0.8');

