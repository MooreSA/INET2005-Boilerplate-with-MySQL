DROP TABLE IF EXISTS tblLinks;

CREATE TABLE tblLinks (
    id INT NOT NULL AUTO_INCREMENT,
    categoryId INT NOT NULL,
    url VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    pinned BOOLEAN NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (categoryId) REFERENCES tblCategories(id)
);

INSERT INTO tblLinks (categoryId, url, title, pinned) VALUES
    (1, 'http://www.google.com', 'Google', TRUE),
    (2, 'http://www.yahoo.com', 'Yahoo', FALSE),
    (3, 'https://www.github.com', 'Github', TRUE),
    (3, 'https://facebook.com', 'facebook', FALSE),
    (3, 'https://nscc.ca', 'NSCC', TRUE),
    (4, 'https://nscc.ca', 'ALPHA', TRUE),
    (4, 'https://nscc.ca', 'ZZZ', TRUE),
    (4, 'https://nscc.ca', 'AAA', TRUE),
    (4, 'https://twitter.com', 'twitter', FALSE),
    (4, 'https://twitter.com', 'c', FALSE),
    (4, 'https://twitter.com', 'a', FALSE),
    (4, 'https://twitter.com', 'b', FALSE);

