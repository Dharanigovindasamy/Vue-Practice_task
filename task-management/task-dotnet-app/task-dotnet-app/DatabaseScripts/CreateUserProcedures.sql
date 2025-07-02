-- Stored Procedure to insert a user
CREATE OR REPLACE PROCEDURE insert_user(
    p_userName VARCHAR,
    p_email VARCHAR,
    p_projectId INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO users ("userName", "email", "ProjectId")
    VALUES (p_userName, p_email, p_projectId);
END;
$$;

-- Function to get users by email
CREATE OR REPLACE FUNCTION get_users_by_email(p_email VARCHAR)
RETURNS TABLE(userId INT, userName VARCHAR, email VARCHAR, ProjectId INT) AS $$
BEGIN
    RETURN QUERY
    SELECT "userId", "userName", "email", "ProjectId"
    FROM users
    WHERE email = p_email;
END;
$$ LANGUAGE plpgsql; 