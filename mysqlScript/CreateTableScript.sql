
CREATE DATABASE IF NOT EXISTS web_market_db;

USE web_market_db;

CREATE TABLE IF NOT EXISTS
`web_market_db`.`user`(
		`id` BINARY(16) NOT NULL, 
		`userName` VARCHAR(50) NOT NULL,
		`email` VARCHAR(100) NOT NULL,
		`password` VARCHAR(100) NOT NULL,
		`passwordSalt` VARCHAR(100) NOT NULL,
        `isActive` BOOLEAN NOT NULL,
		`creationDate` DATETIME NOT NULL,
		`updateTime` DATETIME DEFAULT NULL,
	PRIMARY KEY (`id`),
	INDEX `idx_UserName`(`UserName`),
    INDEX `idx_Email`(`Email`)
  )ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS 
`web_market_db`.`role`(
		`id` BINARY(16) NOT NULL, 
        `name` VARCHAR(50) NOT NULL,
        `isActive` BOOLEAN NOT NULL,
        `creationDate` DATETIME NOT NULL,
        `UpdateTime` DATETIME NULL,
	PRIMARY KEY (`id`),
    INDEX `idx_Name`(`name`)
)ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS 
`web_market_db`.`user_role`(
		`userId` BINARY(16) NOT NULL, 
        `roleId` BINARY(16) NOT NULL, 
	PRIMARY KEY (`userId`,`roleId`),
    FOREIGN KEY(`userId`) REFERENCES `user`(`id`),
    FOREIGN KEY(`roleId`) REFERENCES `role`(`id`)
)ENGINE=InnoDB;
