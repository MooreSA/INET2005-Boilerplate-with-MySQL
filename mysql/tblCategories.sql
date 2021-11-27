DROP TABLE IF EXISTS tblCategories;

CREATE TABLE tblCategories (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    categoryName VARCHAR(100) NOT NULL
);

-- Populate table
INSERT INTO tblCategories (categoryName) VALUES ('Tech');
INSERT INTO tblCategories (categoryName) VALUES ('School');
INSERT INTO tblCategories (categoryName) VALUES ('Play');
INSERT INTO tblCategories (categoryName) VALUES ('Data');