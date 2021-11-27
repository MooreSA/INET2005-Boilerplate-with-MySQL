DROP TABLE IF EXISTS tblUsers;

CREATE TABLE tblUsers (
  `username` varchar(45) NOT NULL,
  `hashedpassword` varchar(200) NOT NULL,
  `salt` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE tblUsers
  ADD PRIMARY KEY (`username`);

INSERT INTO tblUsers (username, hashedpassword, salt) VALUES
  ("test", "brjE69OO04kzMbqyCnAMenSaEyTEsT/ynpjtl+jiBF0=","dClWm2/dkcmqlzSbwoZDkg==");