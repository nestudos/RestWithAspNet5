CREATE TABLE `book` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL,
  `author` varchar(80) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `lauch_date` datetime NOT NULL,
  PRIMARY KEY (`id`)
);
