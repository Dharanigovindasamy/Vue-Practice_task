# Debugging Stored Procedures and .NET Calls

## Debugging the Stored Procedure in PostgreSQL

- Use `RAISE NOTICE` in your procedure to output debug info:

```sql
CREATE OR REPLACE PROCEDURE insert_user(
    p_userName VARCHAR,
    p_email VARCHAR,
    p_projectId INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    RAISE NOTICE 'Inserting user: %, %, %', p_userName, p_email, p_projectId;
    INSERT INTO users ("userName", "email", "ProjectId")
    VALUES (p_userName, p_email, p_projectId);
END;
$$;
```
- Check the output in your SQL client (e.g., pgAdmin, DBeaver, psql).

## Debugging the .NET Code

- Set breakpoints in your controller (e.g., `UserStoredProcedureController`).
- Inspect the parameters and results.
- Check for exceptions in the output window.
- Log the SQL command and parameters if needed.

## Tips
- Make sure the procedure/function exists in the database.
- Use SQL tools to test the procedure directly before calling from .NET. 