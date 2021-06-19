IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'chinwag')
	BEGIN
		CREATE DATABASE [chinwag]
	END