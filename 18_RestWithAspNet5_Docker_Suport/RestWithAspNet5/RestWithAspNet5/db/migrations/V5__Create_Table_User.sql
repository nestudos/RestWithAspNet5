CREATE TABLE `user` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `user_name` varchar(50) NOT NULL,
  `password` varchar(130) NOT NULL,
  `full_name` varchar(120) NOT NULL,
  `refresh_token` varchar(500) NULL,
  `refresh_token_expiry_time` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);
