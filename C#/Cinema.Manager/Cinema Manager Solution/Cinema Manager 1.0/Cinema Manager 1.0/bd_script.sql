-- --------------------------------------------------------
-- Host:                         192.168.1.88
-- Server version:               5.5.8-log - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2014-04-03 18:45:58
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for cinema
DROP DATABASE IF EXISTS `cinema`;
CREATE DATABASE IF NOT EXISTS `cinema` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `cinema`;


-- Dumping structure for table cinema.actores
DROP TABLE IF EXISTS `actores`;
CREATE TABLE IF NOT EXISTS `actores` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `nome_completo` varchar(255) NOT NULL DEFAULT '0',
  `primeiro_nome` varchar(255) NOT NULL DEFAULT '0',
  `apelido` varchar(255) NOT NULL DEFAULT '0',
  `nacionalidade` int(10) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `nome_completo` (`nome_completo`),
  KEY `nacionalidade` (`nacionalidade`),
  CONSTRAINT `FK_actores_nacionalidades` FOREIGN KEY (`nacionalidade`) REFERENCES `nacionalidades` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.actores: ~2 rows (approximately)
/*!40000 ALTER TABLE `actores` DISABLE KEYS */;
INSERT INTO `actores` (`ID`, `nome_completo`, `primeiro_nome`, `apelido`, `nacionalidade`) VALUES
	(1, 'Joao Franco', 'Joao', 'Franco', 1),
	(4, 'Joao Pires', 'Joao', 'Pires', 3);
/*!40000 ALTER TABLE `actores` ENABLE KEYS */;


-- Dumping structure for table cinema.actores_filmes
DROP TABLE IF EXISTS `actores_filmes`;
CREATE TABLE IF NOT EXISTS `actores_filmes` (
  `ID_ACTOR` int(10) NOT NULL,
  `ID_FILME` int(10) NOT NULL,
  KEY `FK_ACTOR_ID` (`ID_ACTOR`),
  KEY `FK_ACTOR_ID_FILME` (`ID_FILME`),
  CONSTRAINT `FK_ACTOR_ID` FOREIGN KEY (`ID_ACTOR`) REFERENCES `actores` (`ID`),
  CONSTRAINT `FK_ACTOR_ID_FILME` FOREIGN KEY (`ID_FILME`) REFERENCES `filmes` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.actores_filmes: ~1 rows (approximately)
/*!40000 ALTER TABLE `actores_filmes` DISABLE KEYS */;
INSERT INTO `actores_filmes` (`ID_ACTOR`, `ID_FILME`) VALUES
	(1, 1);
/*!40000 ALTER TABLE `actores_filmes` ENABLE KEYS */;


-- Dumping structure for table cinema.anuncios
DROP TABLE IF EXISTS `anuncios`;
CREATE TABLE IF NOT EXISTS `anuncios` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `empresa` int(255) NOT NULL DEFAULT '0',
  `duracao` varchar(255) NOT NULL DEFAULT '0',
  `preco` varchar(255) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `empresa` (`empresa`),
  CONSTRAINT `FK_EMPRESA` FOREIGN KEY (`empresa`) REFERENCES `empresas` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.anuncios: ~1 rows (approximately)
/*!40000 ALTER TABLE `anuncios` DISABLE KEYS */;
INSERT INTO `anuncios` (`ID`, `empresa`, `duracao`, `preco`) VALUES
	(1, 1, '1:15', '20');
/*!40000 ALTER TABLE `anuncios` ENABLE KEYS */;


-- Dumping structure for table cinema.anuncios_sessoes
DROP TABLE IF EXISTS `anuncios_sessoes`;
CREATE TABLE IF NOT EXISTS `anuncios_sessoes` (
  `anuncioID` int(10) NOT NULL,
  `sessaoID` int(10) NOT NULL,
  `alias` varchar(50) NOT NULL,
  UNIQUE KEY `alias` (`alias`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.anuncios_sessoes: ~0 rows (approximately)
/*!40000 ALTER TABLE `anuncios_sessoes` DISABLE KEYS */;
/*!40000 ALTER TABLE `anuncios_sessoes` ENABLE KEYS */;


-- Dumping structure for table cinema.bilhete
DROP TABLE IF EXISTS `bilhete`;
CREATE TABLE IF NOT EXISTS `bilhete` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Lugar` int(11) NOT NULL DEFAULT '0',
  `Preço` int(11) NOT NULL DEFAULT '0',
  `TipoID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.bilhete: ~0 rows (approximately)
/*!40000 ALTER TABLE `bilhete` DISABLE KEYS */;
/*!40000 ALTER TABLE `bilhete` ENABLE KEYS */;


-- Dumping structure for table cinema.bilhetetipo
DROP TABLE IF EXISTS `bilhetetipo`;
CREATE TABLE IF NOT EXISTS `bilhetetipo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.bilhetetipo: ~0 rows (approximately)
/*!40000 ALTER TABLE `bilhetetipo` DISABLE KEYS */;
/*!40000 ALTER TABLE `bilhetetipo` ENABLE KEYS */;


-- Dumping structure for table cinema.bilhetevendido
DROP TABLE IF EXISTS `bilhetevendido`;
CREATE TABLE IF NOT EXISTS `bilhetevendido` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BilheteID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.bilhetevendido: ~0 rows (approximately)
/*!40000 ALTER TABLE `bilhetevendido` DISABLE KEYS */;
/*!40000 ALTER TABLE `bilhetevendido` ENABLE KEYS */;


-- Dumping structure for table cinema.cargos
DROP TABLE IF EXISTS `cargos`;
CREATE TABLE IF NOT EXISTS `cargos` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `cargo` varchar(50) NOT NULL DEFAULT '0',
  `permissoes` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.cargos: ~3 rows (approximately)
/*!40000 ALTER TABLE `cargos` DISABLE KEYS */;
INSERT INTO `cargos` (`ID`, `cargo`, `permissoes`) VALUES
	(1, 'funcionario', 1),
	(2, 'gestor', 2),
	(3, 'administrador', 3);
/*!40000 ALTER TABLE `cargos` ENABLE KEYS */;


-- Dumping structure for table cinema.empresas
DROP TABLE IF EXISTS `empresas`;
CREATE TABLE IF NOT EXISTS `empresas` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `nome` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.empresas: ~1 rows (approximately)
/*!40000 ALTER TABLE `empresas` DISABLE KEYS */;
INSERT INTO `empresas` (`ID`, `nome`) VALUES
	(1, 'Universal Pictures');
/*!40000 ALTER TABLE `empresas` ENABLE KEYS */;


-- Dumping structure for table cinema.estados_filmes
DROP TABLE IF EXISTS `estados_filmes`;
CREATE TABLE IF NOT EXISTS `estados_filmes` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `estado` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.estados_filmes: ~3 rows (approximately)
/*!40000 ALTER TABLE `estados_filmes` DISABLE KEYS */;
INSERT INTO `estados_filmes` (`ID`, `estado`) VALUES
	(1, 'Por Exibir'),
	(2, 'Em Exibição'),
	(3, 'Já Exibido');
/*!40000 ALTER TABLE `estados_filmes` ENABLE KEYS */;


-- Dumping structure for table cinema.filmes
DROP TABLE IF EXISTS `filmes`;
CREATE TABLE IF NOT EXISTS `filmes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `titulo` varchar(255) NOT NULL DEFAULT '0',
  `duracao` varchar(255) NOT NULL DEFAULT '0',
  `resumoID` int(10) NOT NULL,
  `duracao_resumo` int(10) NOT NULL,
  `estadoID` int(10) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `titulo` (`titulo`),
  UNIQUE KEY `resumoID` (`resumoID`),
  UNIQUE KEY `duracao_resumo` (`duracao_resumo`),
  KEY `FK_ESTADO_FILME` (`estadoID`),
  CONSTRAINT `FK_ESTADO_FILME` FOREIGN KEY (`estadoID`) REFERENCES `estados_filmes` (`ID`),
  CONSTRAINT `FK_RESUMO_DURACAO` FOREIGN KEY (`duracao_resumo`) REFERENCES `resumos_filmes` (`ID`),
  CONSTRAINT `FK_RESUMO_FILME` FOREIGN KEY (`resumoID`) REFERENCES `resumos_filmes` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.filmes: ~1 rows (approximately)
/*!40000 ALTER TABLE `filmes` DISABLE KEYS */;
INSERT INTO `filmes` (`ID`, `titulo`, `duracao`, `resumoID`, `duracao_resumo`, `estadoID`) VALUES
	(1, 'Avatar', '2:20:00', 1, 1, 1);
/*!40000 ALTER TABLE `filmes` ENABLE KEYS */;


-- Dumping structure for table cinema.funcionarios
DROP TABLE IF EXISTS `funcionarios`;
CREATE TABLE IF NOT EXISTS `funcionarios` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `alias` varchar(50) NOT NULL DEFAULT '0',
  `primeiro_nome` varchar(50) NOT NULL DEFAULT '0',
  `segundo_nome` varchar(50) NOT NULL DEFAULT '0',
  `data_nascimento` varchar(50) NOT NULL DEFAULT '0000-00-00',
  `numero_bi` int(11) NOT NULL DEFAULT '0',
  `cargo` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `alias` (`alias`),
  KEY `foreign_cargo` (`cargo`),
  CONSTRAINT `foreign_cargo` FOREIGN KEY (`cargo`) REFERENCES `cargos` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.funcionarios: ~7 rows (approximately)
/*!40000 ALTER TABLE `funcionarios` DISABLE KEYS */;
INSERT INTO `funcionarios` (`ID`, `alias`, `primeiro_nome`, `segundo_nome`, `data_nascimento`, `numero_bi`, `cargo`) VALUES
	(1, '3-cinema-cinema', 'cinema', 'cinema', '0000-00-00', 0, 3),
	(2, '1-joao-franco', 'Joao', 'Franco', '25-07-1991', 13622841, 1),
	(3, '2-joao-franco', 'Joao', 'Franco', '25-07-1991', 13622841, 2),
	(5, '3-joao-franco', 'Joao', 'Franco', '25-07-1991', 13622841, 3),
	(7, '1-joao-pires', 'Joao', 'Pires', '10-04-1992', 14601239, 1),
	(9, '2-joao-pires', 'Joao', 'Pires', '10-04-1992', 14601239, 2),
	(10, '3-joao-pires', 'Joao', 'Pires', '10-04-1992', 14601239, 3);
/*!40000 ALTER TABLE `funcionarios` ENABLE KEYS */;


-- Dumping structure for table cinema.lista_anuncios
DROP TABLE IF EXISTS `lista_anuncios`;
CREATE TABLE IF NOT EXISTS `lista_anuncios` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `anuncioID` int(50) NOT NULL DEFAULT '0',
  `sessaoID` int(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.lista_anuncios: ~0 rows (approximately)
/*!40000 ALTER TABLE `lista_anuncios` DISABLE KEYS */;
/*!40000 ALTER TABLE `lista_anuncios` ENABLE KEYS */;


-- Dumping structure for table cinema.login
DROP TABLE IF EXISTS `login`;
CREATE TABLE IF NOT EXISTS `login` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `id_alias` int(11) NOT NULL DEFAULT '0',
  `alias` varchar(50) NOT NULL DEFAULT '0',
  `password` text NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `foreign_id_alias` (`id_alias`),
  CONSTRAINT `foreign_id_alias` FOREIGN KEY (`id_alias`) REFERENCES `funcionarios` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.login: ~7 rows (approximately)
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` (`ID`, `id_alias`, `alias`, `password`) VALUES
	(1, 2, '1-joao-franco', '136joaofranco'),
	(2, 7, '1-joao-pires', '146joaopires'),
	(3, 3, '2-joao-franco', '136joaofranco'),
	(4, 9, '2-joao-pires', '146joaopires'),
	(5, 5, '3-joao-franco', '136joaofranco'),
	(6, 10, '3-joao-pires', '146joaopires'),
	(10, 1, '3-cinema-cinema', 'cinema');
/*!40000 ALTER TABLE `login` ENABLE KEYS */;


-- Dumping structure for table cinema.marcacao
DROP TABLE IF EXISTS `marcacao`;
CREATE TABLE IF NOT EXISTS `marcacao` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `SalaID` int(11) NOT NULL DEFAULT '0',
  `Lugares` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.marcacao: ~0 rows (approximately)
/*!40000 ALTER TABLE `marcacao` DISABLE KEYS */;
/*!40000 ALTER TABLE `marcacao` ENABLE KEYS */;


-- Dumping structure for table cinema.nacionalidades
DROP TABLE IF EXISTS `nacionalidades`;
CREATE TABLE IF NOT EXISTS `nacionalidades` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `nacionalidade` varchar(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.nacionalidades: ~4 rows (approximately)
/*!40000 ALTER TABLE `nacionalidades` DISABLE KEYS */;
INSERT INTO `nacionalidades` (`ID`, `nacionalidade`) VALUES
	(1, 'Portuguesa'),
	(2, 'Americana'),
	(3, 'Francesa'),
	(4, 'Espanhola');
/*!40000 ALTER TABLE `nacionalidades` ENABLE KEYS */;


-- Dumping structure for table cinema.productores
DROP TABLE IF EXISTS `productores`;
CREATE TABLE IF NOT EXISTS `productores` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `nome_completo` varchar(50) NOT NULL DEFAULT '0',
  `primeiro_nome` varchar(50) NOT NULL DEFAULT '0',
  `apelido` varchar(50) NOT NULL DEFAULT '0',
  `nacionalidade` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `nome-completo` (`nome_completo`),
  KEY `nacionalidade` (`nacionalidade`),
  CONSTRAINT `FK_produtores_nacionalidades` FOREIGN KEY (`nacionalidade`) REFERENCES `nacionalidades` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.productores: ~1 rows (approximately)
/*!40000 ALTER TABLE `productores` DISABLE KEYS */;
INSERT INTO `productores` (`ID`, `nome_completo`, `primeiro_nome`, `apelido`, `nacionalidade`) VALUES
	(1, 'João Franco', 'João', 'Franco', 1);
/*!40000 ALTER TABLE `productores` ENABLE KEYS */;


-- Dumping structure for table cinema.productores_filmes
DROP TABLE IF EXISTS `productores_filmes`;
CREATE TABLE IF NOT EXISTS `productores_filmes` (
  `ID_PRODUTOR` int(10) NOT NULL,
  `ID_FILME` int(10) NOT NULL,
  KEY `FK_PRODUTOR_ID_FILME` (`ID_FILME`),
  KEY `FK_PRODUTOR_ID` (`ID_PRODUTOR`),
  CONSTRAINT `FK_PRODUTOR_ID` FOREIGN KEY (`ID_PRODUTOR`) REFERENCES `productores` (`ID`),
  CONSTRAINT `FK_PRODUTOR_ID_FILME` FOREIGN KEY (`ID_FILME`) REFERENCES `filmes` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.productores_filmes: ~1 rows (approximately)
/*!40000 ALTER TABLE `productores_filmes` DISABLE KEYS */;
INSERT INTO `productores_filmes` (`ID_PRODUTOR`, `ID_FILME`) VALUES
	(1, 1);
/*!40000 ALTER TABLE `productores_filmes` ENABLE KEYS */;


-- Dumping structure for table cinema.realizadores
DROP TABLE IF EXISTS `realizadores`;
CREATE TABLE IF NOT EXISTS `realizadores` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `nome_completo` varchar(50) NOT NULL DEFAULT '0',
  `primeiro_nome` varchar(50) NOT NULL DEFAULT '0',
  `apelido` varchar(50) NOT NULL DEFAULT '0',
  `nacionalidade` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `nome-completo` (`nome_completo`),
  KEY `nacionalidade` (`nacionalidade`),
  CONSTRAINT `FK_realizadores_nacionalidades` FOREIGN KEY (`nacionalidade`) REFERENCES `nacionalidades` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.realizadores: ~1 rows (approximately)
/*!40000 ALTER TABLE `realizadores` DISABLE KEYS */;
INSERT INTO `realizadores` (`ID`, `nome_completo`, `primeiro_nome`, `apelido`, `nacionalidade`) VALUES
	(1, 'João Franco', 'João', 'Franco', 2);
/*!40000 ALTER TABLE `realizadores` ENABLE KEYS */;


-- Dumping structure for table cinema.realizadores_filmes
DROP TABLE IF EXISTS `realizadores_filmes`;
CREATE TABLE IF NOT EXISTS `realizadores_filmes` (
  `ID_REALIZADOR` int(10) NOT NULL,
  `ID_FILME` int(10) NOT NULL,
  KEY `FK_REALIZADOR_ID_FILME` (`ID_FILME`),
  KEY `FK_REALIZADOR_ID` (`ID_REALIZADOR`),
  CONSTRAINT `FK_REALIZADOR_ID` FOREIGN KEY (`ID_REALIZADOR`) REFERENCES `realizadores` (`ID`),
  CONSTRAINT `FK_REALIZADOR_ID_FILME` FOREIGN KEY (`ID_FILME`) REFERENCES `filmes` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.realizadores_filmes: ~1 rows (approximately)
/*!40000 ALTER TABLE `realizadores_filmes` DISABLE KEYS */;
INSERT INTO `realizadores_filmes` (`ID_REALIZADOR`, `ID_FILME`) VALUES
	(1, 1);
/*!40000 ALTER TABLE `realizadores_filmes` ENABLE KEYS */;


-- Dumping structure for table cinema.resumos_filmes
DROP TABLE IF EXISTS `resumos_filmes`;
CREATE TABLE IF NOT EXISTS `resumos_filmes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL DEFAULT '0',
  `duracao` varchar(255) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `nome` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.resumos_filmes: ~1 rows (approximately)
/*!40000 ALTER TABLE `resumos_filmes` DISABLE KEYS */;
INSERT INTO `resumos_filmes` (`ID`, `nome`, `duracao`) VALUES
	(1, 'Avatar Trailer', '2:30');
/*!40000 ALTER TABLE `resumos_filmes` ENABLE KEYS */;


-- Dumping structure for table cinema.salas
DROP TABLE IF EXISTS `salas`;
CREATE TABLE IF NOT EXISTS `salas` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `capacidade` int(11) NOT NULL DEFAULT '0',
  `alias` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `alias` (`alias`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.salas: ~4 rows (approximately)
/*!40000 ALTER TABLE `salas` DISABLE KEYS */;
INSERT INTO `salas` (`ID`, `capacidade`, `alias`) VALUES
	(3, 9, '3/9'),
	(4, 10, '4/10'),
	(7, 10, '7/10'),
	(9, 443, '9/443');
/*!40000 ALTER TABLE `salas` ENABLE KEYS */;


-- Dumping structure for table cinema.sessoes
DROP TABLE IF EXISTS `sessoes`;
CREATE TABLE IF NOT EXISTS `sessoes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `salaID` int(11) NOT NULL DEFAULT '0',
  `filmeID` int(11) NOT NULL DEFAULT '0',
  `horario` varchar(50) NOT NULL DEFAULT '0',
  `duracao` varchar(50) NOT NULL DEFAULT '0',
  `alias` varchar(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `alias` (`alias`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table cinema.sessoes: ~0 rows (approximately)
/*!40000 ALTER TABLE `sessoes` DISABLE KEYS */;
/*!40000 ALTER TABLE `sessoes` ENABLE KEYS */;


-- Dumping structure for procedure cinema.SP_ACTOR_ALTERAR
DROP PROCEDURE IF EXISTS `SP_ACTOR_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_ACTOR_ALTERAR`(IN `_id` INT, IN `_nome_completo` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_apelido` VARCHAR(50), IN `_nacionalidade` INT)
BEGIN
UPDATE actores SET nome_completo=_nome_completo, primeiro_nome=_primeiro_nome, apelido=_apelido, nacionalidade=_nacionalidade WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_ACTOR_INSERIR
DROP PROCEDURE IF EXISTS `SP_ACTOR_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_ACTOR_INSERIR`(IN `_nome_completo` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_apelido` VARCHAR(50), IN `_nacionalidade` INT)
BEGIN
INSERT INTO actores VALUES(null, _nome_completo, _primeiro_nome, _apelido, _nacionalidade);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_ACTOR_REMOVER
DROP PROCEDURE IF EXISTS `SP_ACTOR_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_ACTOR_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM actores WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FILMES_SELECIONAR
DROP PROCEDURE IF EXISTS `SP_FILMES_SELECIONAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FILMES_SELECIONAR`()
BEGIN
SELECT filmes.ID, filmes.titulo, filmes.duracao, resumos_filmes.nome, resumos_filmes.duracao AS duracao_resumo, estados_filmes.estado FROM filmes INNER JOIN resumos_filmes ON filmes.resumoID = resumos_filmes.ID INNER JOIN resumos_filmes AS res ON filmes.duracao_resumo = res.ID INNER JOIN estados_filmes ON filmes.estadoID = estados_filmes.ID;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FILME_ALTERAR
DROP PROCEDURE IF EXISTS `SP_FILME_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FILME_ALTERAR`(IN `_id` INT, IN `_titulo` VARCHAR(50), IN `_duracao` VARCHAR(50), IN `_resumo` INT, IN `_duracao_resumo` INT, IN `_estado` INT)
BEGIN
UPDATE filmes SET titulo=_titulo, duracao=_duracao, resumoID=_resumo, duracao_resumo=_duracao_resumo, estadoID=_estado WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FILME_INSERIR
DROP PROCEDURE IF EXISTS `SP_FILME_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FILME_INSERIR`(IN `_titulo` VARCHAR(50), IN `_duracao` VARCHAR(50), IN `_resumo` INT, IN `_duracao_resumo` INT, IN `_estado` INT)
BEGIN
INSERT INTO filmes VALUES(null, _titulo, _duracao, _resumo, _duracao_resumo, _estado);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FILME_REMOVER
DROP PROCEDURE IF EXISTS `SP_FILME_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FILME_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM filmes WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FUNCIONARIO_ALTERAR
DROP PROCEDURE IF EXISTS `SP_FUNCIONARIO_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FUNCIONARIO_ALTERAR`(IN `_id` INT, IN `_alias` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_segundo_nome` VARCHAR(50), IN `_data_nascimento` VARCHAR(50), IN `_numero_bi` INT, IN `_cargo` VARCHAR(50))
BEGIN
UPDATE funcionarios SET alias=_alias,primeiro_nome=_primeiro_nome, segundo_nome=_segundo_nome, data_nascimento=_data_nascimento, numero_bi=_numero_bi, cargo=_cargo WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FUNCIONARIO_INSERIR
DROP PROCEDURE IF EXISTS `SP_FUNCIONARIO_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FUNCIONARIO_INSERIR`(IN `_alias` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_ultimo_nome` VARCHAR(50), IN `_data_nascimento` VARCHAR(50), IN `_numero_bi` INT, IN `_cargo` INT)
BEGIN
INSERT INTO funcionarios VALUES(null, _alias, _primeiro_nome, _ultimo_nome, _data_nascimento, _numero_bi, _cargo);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_FUNCIONARIO_REMOVER
DROP PROCEDURE IF EXISTS `SP_FUNCIONARIO_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_FUNCIONARIO_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM funcionarios WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_LOGIN_ALTERAR
DROP PROCEDURE IF EXISTS `SP_LOGIN_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_LOGIN_ALTERAR`(IN `_id_alias` INT, IN `_alias` VARCHAR(50), IN `_password` VARCHAR(50))
BEGIN
UPDATE login SET alias=_alias, `password`=_password WHERE id_alias=_id_alias;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_LOGIN_INSERIR
DROP PROCEDURE IF EXISTS `SP_LOGIN_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_LOGIN_INSERIR`(IN `_id_alias` INT, IN `_alias` VARCHAR(50), IN `_password` TEXT)
BEGIN
INSERT INTO login VALUES(null, _id_alias, _alias, _password);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_LOGIN_REMOVER
DROP PROCEDURE IF EXISTS `SP_LOGIN_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_LOGIN_REMOVER`(IN `_id_alias` INT)
BEGIN
DELETE FROM login WHERE id_alias=_id_alias;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_NACIONALIDADE_ALTERAR
DROP PROCEDURE IF EXISTS `SP_NACIONALIDADE_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_NACIONALIDADE_ALTERAR`(IN `_id` INT, IN `_nacionalidade` VARCHAR(50))
BEGIN
UPDATE nacionalidades SET nacionalidade=_nacionalidade WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_NACIONALIDADE_INSERIR
DROP PROCEDURE IF EXISTS `SP_NACIONALIDADE_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_NACIONALIDADE_INSERIR`(IN `_nacionalidade` VARCHAR(50))
BEGIN
INSERT INTO nacionalidades VALUES(null, _nacionalidade);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_NACIONALIDADE_REMOVER
DROP PROCEDURE IF EXISTS `SP_NACIONALIDADE_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_NACIONALIDADE_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM nacionalidades WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_PRODUCTOR_ALTERAR
DROP PROCEDURE IF EXISTS `SP_PRODUCTOR_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_PRODUCTOR_ALTERAR`(IN `_id` INT, IN `_nome_completo` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_apelido` VARCHAR(50), IN `_nacionalidade` INT)
BEGIN
UPDATE productores SET nome_completo=_nome_completo, primeiro_nome=_primeiro_nome, apelido=_apelido, nacionalidade=_nacionalidade WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_PRODUCTOR_INSERIR
DROP PROCEDURE IF EXISTS `SP_PRODUCTOR_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_PRODUCTOR_INSERIR`(IN `_nome_completo` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_apelido` VARCHAR(50), IN `_nacionalidade` INT)
BEGIN
INSERT INTO productores VALUES(null, _nome_completo, _primeiro_nome, _apelido, _nacionalidade);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_PRODUCTOR_REMOVER
DROP PROCEDURE IF EXISTS `SP_PRODUCTOR_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_PRODUCTOR_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM productores WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_REALIZADOR_ALTERAR
DROP PROCEDURE IF EXISTS `SP_REALIZADOR_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_REALIZADOR_ALTERAR`(IN `_id` INT, IN `_nome_completo` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_apelido` VARCHAR(50), IN `_nacionalidade` INT)
BEGIN
UPDATE realizadores SET nome_completo=_nome_completo, primeiro_nome=_primeiro_nome, apelido=_apelido, nacionalidade=_nacionalidade WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_REALIZADOR_INSERIR
DROP PROCEDURE IF EXISTS `SP_REALIZADOR_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_REALIZADOR_INSERIR`(IN `_nome_completo` VARCHAR(50), IN `_primeiro_nome` VARCHAR(50), IN `_apelido` VARCHAR(50), IN `_nacionalidade` INT)
BEGIN
INSERT INTO realizadores VALUES(null, _nome_completo, _primeiro_nome, _apelido, _nacionalidade);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_REALIZADOR_REMOVER
DROP PROCEDURE IF EXISTS `SP_REALIZADOR_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_REALIZADOR_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM realizadores WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_ResumoFilme_REMOVER
DROP PROCEDURE IF EXISTS `SP_ResumoFilme_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_ResumoFilme_REMOVER`(IN `_nome` VARCHAR(50))
BEGIN
DELETE FROM resumos_filmes WHERE nome=_nome;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_RESUMO_ALTERAR
DROP PROCEDURE IF EXISTS `SP_RESUMO_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_RESUMO_ALTERAR`(IN `_id` INT, IN `_nome` VARCHAR(50), IN `_duracao` VARCHAR(50))
BEGIN
UPDATE resumos_filmes SET nome=_nome, duracao=_duracao WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_RESUMO_INSERIR
DROP PROCEDURE IF EXISTS `SP_RESUMO_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_RESUMO_INSERIR`(IN `_nome` VARCHAR(50), IN `_duracao` VARCHAR(50))
BEGIN
INSERT INTO resumos_filmes VALUES(null, _nome, _duracao);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_RESUMO_REMOVER
DROP PROCEDURE IF EXISTS `SP_RESUMO_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_RESUMO_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM resumos_filmes WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_SALAS_ALTERAR
DROP PROCEDURE IF EXISTS `SP_SALAS_ALTERAR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_SALAS_ALTERAR`(IN `_id` INT, IN `_capacidade` INT)
BEGIN
UPDATE salas SET capacidade=_capacidade WHERE ID=_id;
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_SALAS_INSERIR
DROP PROCEDURE IF EXISTS `SP_SALAS_INSERIR`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_SALAS_INSERIR`(IN `_capacidade` INT)
BEGIN
INSERT INTO salas VALUES(null, _capacidade);
END//
DELIMITER ;


-- Dumping structure for procedure cinema.SP_SALAS_REMOVER
DROP PROCEDURE IF EXISTS `SP_SALAS_REMOVER`;
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `SP_SALAS_REMOVER`(IN `_id` INT)
BEGIN
DELETE FROM salas WHERE ID=_id;
END//
DELIMITER ;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
