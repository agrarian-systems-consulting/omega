BEGIN TRANSACTION;
DROP TABLE IF EXISTS `_Definitions`;
CREATE TABLE IF NOT EXISTS `_Definitions` (
	`IdDefinitions`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NomCat`	TEXT,
	`NameCat`	TEXT
);
INSERT INTO `_Definitions` (IdDefinitions,NomCat,NameCat) VALUES (1,'Produits','Outputs'),
 (2,'Charges','Inputs'),
 (3,'Externalites','Externalities'),
 (4,'Bestiaux','Animals'),
 (5,'Calendriers','Calendars'),
 (6,'ChargesStruct','Fixed_Assets'),
 (7,'Rec_Div','Misc_Outputs'),
 (8,'Dep_Div','Misc_Inputs'),
 (9,'Rec_Fam','Fam_Outputs'),
 (10,'Dep_Fam','Fam_Inputs'),
 (11,'Variables','Variables'),
 (12,'Dico','Dictionary'),
 (13,'Classification','Classification'),
 (14,'EtatSortie','Custom Form'),
 (15,'SerieComp','ComparForm'),
 (16,'Indicat','Indicator'),
 (17,'Repart','Repart'),
 (18,'Conversion','Conversion');
DROP TABLE IF EXISTS `_Ateliers`;
CREATE TABLE IF NOT EXISTS `_Ateliers` (
	`IdAteliers`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NomAte`	TEXT,
	`NameAte`	TEXT
);
INSERT INTO `_Ateliers` (IdAteliers,NomAte,NameAte) VALUES (1,'Culture','Annual Crop'),
 (2,'Animaux','Animals'),
 (3,'Perennes','Tree_Crops'),
 (4,'Pluriannuelles','Perennials');
DROP TABLE IF EXISTS `_Ate_ListItem`;
CREATE TABLE IF NOT EXISTS `_Ate_ListItem` (
	`IdAteListItem`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NomAteListItem`	TEXT,
	`NameAteListItem`	TEXT
);
INSERT INTO `_Ate_ListItem` (IdAteListItem,NomAteListItem,NameAteListItem) VALUES (1,'Produit','Outputs'),
 (2,'Charge','Inputs'),
 (3,'ChargeVolume','Input_Volume'),
 (4,'Externalite','Externality'),
 (5,'Avance','Advance'),
 (6,'Immo_Ent','Fix_Ass_Farm'),
 (7,'Immo_Fam','Fix_Ass_Fam'),
 (8,'Travail','Labour'),
 (9,'Pied_Ha','Tree_ha'),
 (10,'Prod_Pied','Output_Tree'),
 (11,'Ch_Pied','Input_Tree');
DROP TABLE IF EXISTS `_Aleas`;
CREATE TABLE IF NOT EXISTS `_Aleas` (
	`IdAleas`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NomAlea`	TEXT,
	`NameHazard`	TEXT
);
INSERT INTO `_Aleas` (IdAleas,NomAlea,NameHazard) VALUES (1,'Prix Prod Tend','Price Output Tend'),
 (2,'Prix Charge Tend','Price Input Tend'),
 (3,'Prix Prod Scen','Price Output Scen'),
 (4,'Prix Charge Scen','Price Input Scen'),
 (5,'Qte Prod Tend','Qty Output Tend'),
 (6,'Qte Charge Tend','Qty Input Tend'),
 (7,'Qte Prod Scen','Qty Output Scen'),
 (8,'Qte Charge Scen','Qty Input Scen'),
 (9,'Qte Ext Tend','Qty Ext Tend'),
 (10,'Qte Ext Scen','Qty Ext Scen');
DROP TABLE IF EXISTS `_Agri_ListValeur`;
CREATE TABLE IF NOT EXISTS `_Agri_ListValeur` (
	`IdAgriValeur`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NomAgriValeur`	TEXT,
	`NameAgriVal`	TEXT
);
INSERT INTO `_Agri_ListValeur` (IdAgriValeur,NomAgriValeur,NameAgriVal) VALUES (1,'Produits','Outputs'),
 (2,'Charges','Inputs'),
 (3,'ChStruct','Fixed_Ass'),
 (4,'RecDiv','Misc_Rev'),
 (5,'DepDiv','Misc_Exp'),
 (6,'RecFam','Fam_Rev'),
 (7,'DepFam','Fam_Exp'),
 (8,'ExtPos','ExtPos'),
 (9,'ExtNeg','ExtNeg'),
 (10,'Variable','Variable'),
 (11,'StockIni','StockIni');
DROP TABLE IF EXISTS `Variable`;
CREATE TABLE IF NOT EXISTS `Variable` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Categorie`	TEXT,
	`Nom`	TEXT,
	`IdSysUnite`	INTEGER
);
CREATE TABLE IF NOT EXISTS `VILLAGE` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NOM`	TEXT,
	`ID_REGROUP`	INTEGER,
	`CAN`	TEXT,
	`DEP`	TEXT,
	`PROV`	TEXT,
	`LAT`	TEXT,
	`LON`	TEXT,
	`counter`	TEXT,
	`fullname`	TEXT,
	`ID2`	int
);
DROP TABLE IF EXISTS `USAGE_FC`;
CREATE TABLE IF NOT EXISTS `USAGE_FC` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`id_usage`	INTEGER,
	`comment`	TEXT,
	`id_create`	TEXT,
	`id_reserv`	TEXT
);
DROP TABLE IF EXISTS `Type_Exploi`;
CREATE TABLE IF NOT EXISTS `Type_Exploi` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT
);
INSERT INTO `Type_Exploi` (ID,Nom) VALUES (1,'Principale'),
 (2,'Secondaire');
DROP TABLE IF EXISTS `Type`;
CREATE TABLE IF NOT EXISTS `Type` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT NOT NULL,
	`Note`	TEXT
);
INSERT INTO `Type` (ID,Nom,Note) VALUES (1,'Crop',NULL),
 (2,'Elevage',NULL),
 (3,'Hunt',NULL),
 (4,'Perenial',NULL),
 (5,'Animals',NULL),
 (6,'Pluriannual',NULL),
 (7,'Working farm',NULL),
 (8,'Fishing',NULL),
 (9,'Picking',NULL),
 (10,'Main Map',NULL),
 (11,'last',NULL),
 (12,'Tree',NULL),
 (13,'PFNL',NULL),
 (14,'PFNL 2',NULL);
DROP TABLE IF EXISTS `Travail`;
CREATE TABLE IF NOT EXISTS `Travail` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdPeriode`	INTEGER,
	`Nom_Periode`	TEXT,
	`IdActivite`	INTEGER,
	`IdExploitation`	INTEGER,
	`Qte_Av1_H`	REAL,
	`Qte_1_H`	REAL,
	`Qte_Av1_F`	REAL,
	`Qte_1_F`	REAL,
	`Qte_Av1_T`	REAL,
	`Qte_1_T`	REAL,
	`Qte_2_F`	REAL,
	`Qte_2_H`	REAL,
	`Qte_2_T`	REAL,
	`Qte_3_F`	REAL,
	`Qte_3_H`	REAL,
	`Qte_3_T`	REAL,
	`Qte_4_F`	REAL,
	`Qte_4_H`	REAL,
	`Qte_4_T`	REAL,
	`Qte_5_F`	REAL,
	`Qte_5_H`	REAL,
	`Qte_5_T`	REAL,
	`Qte_6_H`	REAL,
	`Qte_6_T`	REAL,
	`Qte_6_F`	REAL,
	`Qte_7_H`	REAL,
	`Qte_7_T`	REAL,
	`Qte_8_F`	REAL,
	`Qte_8_H`	REAL,
	`Qte_8_T`	REAL,
	`Qte_9_F`	REAL,
	`Qte_9_H`	REAL,
	`Qte_10_T`	REAL,
	`Qte_10_F`	REAL,
	`Qte_10_H`	REAL,
	`Qte_9_T`	REAL,
	`Qte_11_F`	REAL,
	`Qte_11_H`	REAL,
	`Qte_11_T`	REAL,
	`Qte_12_F`	REAL,
	`Qte_12_H`	REAL,
	`Qte_12_T`	REAL,
	`Qte_13_F`	REAL,
	`Qte_13_H`	REAL,
	`Qte_13_T`	REAL,
	`Qte_14_F`	REAL,
	`Qte_14_H`	REAL,
	`Qte_14_T`	REAL,
	`Qte_15_F`	REAL,
	`Qte_15_H`	REAL,
	`Qte_15_T`	REAL
);
DROP TABLE IF EXISTS `TYPELIEU`;
CREATE TABLE IF NOT EXISTS `TYPELIEU` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`TYPE`	TEXT
);
INSERT INTO `TYPELIEU` (ID,TYPE) VALUES (1,'Antenne'),
 (2,'Case de passage'),
 (3,'Pont');
DROP TABLE IF EXISTS `TVA`;
CREATE TABLE IF NOT EXISTS `TVA` (
	`IdTva`	INTEGER,
	`Nom`	TEXT NOT NULL,
	`Taux`	TEXT,
	`Defaut`	TEXT,
	`Immo`	TEXT,
	PRIMARY KEY(`IdTva`)
);
INSERT INTO `TVA` (IdTva,Nom,Taux,Defaut,Immo) VALUES (1,'sans','0','  0','0'),
 (2,'normale','19.6','  1','1');
DROP TABLE IF EXISTS `TRACEPT`;
CREATE TABLE IF NOT EXISTS `TRACEPT` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`X`	TEXT,
	`Y`	TEXT,
	`ALT`	TEXT,
	`ID_TRACE`	INTEGER,
	`DATE`	TEXT
);
DROP TABLE IF EXISTS `TRACE`;
CREATE TABLE IF NOT EXISTS `TRACE` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`DATE`	TEXT,
	`NOM`	TEXT,
	`INPUT_DATE`	TEXT,
	`INPUT_ID`	TEXT,
	`ID_VILLAGE`	TEXT,
	`ID_ENQUETEUR`	TEXT,
	`REMARQUE`	TEXT
);
DROP TABLE IF EXISTS `SystemeUnite`;
CREATE TABLE IF NOT EXISTS `SystemeUnite` (
	`IdSysUnit`	INTEGER,
	`UAte`	TEXT,
	`UEnt`	TEXT,
	`UGlobal`	TEXT,
	`Ratio21`	TEXT,
	`Ratio32`	TEXT,
	`Monnaie`	TEXT,
	PRIMARY KEY(`IdSysUnit`)
);
INSERT INTO `SystemeUnite` (IdSysUnit,UAte,UEnt,UGlobal,Ratio21,Ratio32,Monnaie) VALUES (1,'€','€','K€','1',' 1000','Oui'),
 (2,'q','q','T','1',' 10','Non'),
 (3,'T','T','T','1',' 1','Non'),
 (4,'ha','ha','ha','1',' 1','Non'),
 (5,'L','L','L','1',' 1','Non'),
 (6,'kg','kg','kg','1',' 1','Non'),
 (7,'1','1','1','1',' 1','Non'),
 (8,'KwH','KwH','kwH','1',' 1','Non');
DROP TABLE IF EXISTS `SHP_Info`;
CREATE TABLE IF NOT EXISTS `SHP_Info` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`IdActivite`	INTEGER,
	`MainMap`	TEXT,
	`path`	TEXT,
	`Nom`	TEXT,
	`type`	TEXT,
	`Position`	INTEGER,
	`Color_ARGB`	INTEGER,
	`Code_point`	TEXT
);
DROP TABLE IF EXISTS `Result_Calcul`;
CREATE TABLE IF NOT EXISTS `Result_Calcul` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Table_Origine`	INTEGER,
	`Nom`	INTEGER,
	`Annee`	INTEGER,
	`Valeur`	INTEGER,
	`IdExploitations`	INTEGER,
	FOREIGN KEY(`IdExploitations`) REFERENCES `Exploitation`(`ID`)
);
DROP TABLE IF EXISTS `Projet`;
CREATE TABLE IF NOT EXISTS `Projet` (
	`NoProjet`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NomFichier`	TEXT,
	`Auteur`	TEXT,
	`Pays`	TEXT
);
INSERT INTO `Projet` (NoProjet,NomFichier,Auteur,Pays) VALUES (1,'base initiale','','');
DROP TABLE IF EXISTS `Produits`;
CREATE TABLE IF NOT EXISTS `Produits` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdSysUnit`	INTEGER,
	`Prix`	TEXT,
	`idTva`	INTEGER,
	`IdDefCateg`	INTEGER,
	`Notes`	TEXT,
	`IdQuantiteProd`	INTEGER
);
DROP TABLE IF EXISTS `Prod_Quantite`;
CREATE TABLE IF NOT EXISTS `Prod_Quantite` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdProduits`	INTEGER,
	`Quantite_Avant_1`	INTEGER,
	`Quantite_1`	INTEGER,
	`IdActivite`	INTEGER
);
DROP TABLE IF EXISTS `Prod_Pluriannuelle`;
CREATE TABLE IF NOT EXISTS `Prod_Pluriannuelle` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Quantite_Avant_1`	TEXT,
	`1`	TEXT,
	`2`	TEXT,
	`3`	TEXT,
	`4`	TEXT,
	`IdActivite`	INTEGER,
	`IdProduits`	INTEGER
);
DROP TABLE IF EXISTS `Prod_Perenne`;
CREATE TABLE IF NOT EXISTS `Prod_Perenne` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdProduits`	INTEGER,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT,
	`Quantite_Avant_1`	INTEGER,
	`IdActivite`	INTEGER
);DROP TABLE IF EXISTS `ProdImmo`;
CREATE TABLE IF NOT EXISTS `ProdImmo` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Duree (j)`	INTEGER,
	`Debut`	INTEGER,
	`Avant_1`	INTEGER,
	`_1`	INTEGER,
	`Type`	TEXT
);
DROP TABLE IF EXISTS `PiedHa`;
CREATE TABLE IF NOT EXISTS `PiedHa` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdActivite`	INTEGER,
	`Qte_Av_1`	TEXT,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT
);
DROP TABLE IF EXISTS `PERSON`;
CREATE TABLE IF NOT EXISTS `PERSON` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NOM`	TEXT
);
DROP TABLE IF EXISTS `OTHERGPS`;
CREATE TABLE IF NOT EXISTS `OTHERGPS` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`DATE`	TEXT,
	`ID_ENQUETE`	INTEGER,
	`REMARQUE`	TEXT,
	`ID_VILLAGE`	INTEGER,
	`INPUT_DATE`	TEXT,
	`INPUT_ID`	TEXT,
	`ID_TYPE`	INTEGER,
	`id_gps`	INTEGER
);
DROP TABLE IF EXISTS `OCCUP_AC`;
CREATE TABLE IF NOT EXISTS `OCCUP_AC` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`ID_NATURE`	INTEGER,
	`ID_ACTIVIT`	INTEGER,
	`DETAIL`	TEXT,
	`ID_OCC_SPA`	TEXT,
	`ID_PARTIE`	TEXT,
	`IDS_USAGES`	TEXT,
	`AGRI_SURF`	TEXT
);
DROP TABLE IF EXISTS `OCCUP`;
CREATE TABLE IF NOT EXISTS `OCCUP` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`DATE`	TEXT,
	`ID_SHP`	INTEGER,
	`ID_ENQUETEUR`	INTEGER,
	`REMARQUE`	TEXT,
	`NB_MAISON`	TEXT,
	`NB_OCCUPANT`	TEXT,
	`ID_VILLAGE`	TEXT,
	`DESCRIPTION`	TEXT,
	`INPUT_DATE`	TEXT,
	`INPUT_ID`	TEXT,
	`ID_TYPO`	TEXT,
	`LIGNAGE`	TEXT,
	`id_gps`	TEXT
);
INSERT INTO `OCCUP` (ID,DATE,ID_SHP,ID_ENQUETEUR,REMARQUE,NB_MAISON,NB_OCCUPANT,ID_VILLAGE,DESCRIPTION,INPUT_DATE,INPUT_ID,ID_TYPO,LIGNAGE,id_gps) VALUES (1,'jeudi 24 mai 2012',11,-1,'','0','0','0',NULL,NULL,NULL,NULL,NULL,NULL);
DROP TABLE IF EXISTS `Notes`;
CREATE TABLE IF NOT EXISTS `Notes` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Tables`	TEXT,
	`Valeur`	TEXT
);
DROP TABLE IF EXISTS `LIGNAGE`;
CREATE TABLE IF NOT EXISTS `LIGNAGE` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`id_clan`	INTEGER,
	`nom`	TEXT,
	`chef`	TEXT
);
DROP TABLE IF EXISTS `Item_Pied`;
CREATE TABLE IF NOT EXISTS `Item_Pied` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdActivite`	INTEGER,
	`IdProduits`	INTEGER,
	`IdCharges`	INTEGER,
	`Qte_Av_1`	TEXT,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT
);
DROP TABLE IF EXISTS `Item_ImmoGlobale`;
CREATE TABLE IF NOT EXISTS `Item_ImmoGlobale` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`Name`	TEXT
);
INSERT INTO `Item_ImmoGlobale` (ID,Nom,Name) VALUES (1,'V. Résiduelle
','Residual value'),
 (2,'Amortissement','Depreciation'),
 (3,'Achat','Buy'),
 (4,'Plus Value','Surplus'),
 (5,'Moins Value','Capital loss'),
 (6,'Production d''Immo','Fixed assets production'),
 (7,'Revente','Sell');
DROP TABLE IF EXISTS `Info_Point`;
CREATE TABLE IF NOT EXISTS `Info_Point` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Code_Point`	TEXT,
	`Occup1`	TEXT,
	`Occup2`	TEXT,
	`field1`	TEXT,
	`field2`	TEXT,
	`field3`	TEXT,
	`field4`	TEXT,
	`field5`	TEXT
);
INSERT INTO `Info_Point` (ID,Code_Point,Occup1,Occup2,field1,field2,field3,field4,field5) VALUES (7,'Ble','Culture','Quinoa 2017',NULL,NULL,NULL,NULL,NULL);
DROP TABLE IF EXISTS `Indicateur`;
CREATE TABLE IF NOT EXISTS `Indicateur` (
	`NoIndic`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`NoCateg`	TEXT,
	`IdDefinitions`	INTEGER,
	`type`	TEXT,
	`NoSys`	INTEGER,
	`NbBranche`	INTEGER,
	`NbBoucle`	INTEGER,
	`Notes`	TEXT
);
DROP TABLE IF EXISTS `Immo_PetitMateriel`;
CREATE TABLE IF NOT EXISTS `Immo_PetitMateriel` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`Year`	INTEGER,
	`Value`	TEXT,
	`IdTVA`	INTEGER
);
DROP TABLE IF EXISTS `INFRA`;
CREATE TABLE IF NOT EXISTS `INFRA` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`ID_SOCIO`	INTEGER,
	`ID_INFRA`	INTEGER,
	`TERMINE`	TEXT,
	`ENCOURS`	TEXT,
	`DELABRE`	TEXT
);
DROP TABLE IF EXISTS `GPS`;
CREATE TABLE IF NOT EXISTS `GPS` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`CODE`	TEXT,
	`Y`	TEXT,
	`X`	TEXT,
	`ALT`	TEXT,
	`DATE`	TEXT,
	`import_name`	TEXT,
	`isImport`	TEXT
);
DROP TABLE IF EXISTS `Font`;
CREATE TABLE IF NOT EXISTS `Font` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`taille`	REAL,
	`type`	TEXT
);
INSERT INTO `Font` (ID,Nom,taille,type) VALUES (1,'Microsoft Sans Serif','14,25','Regular');
DROP TABLE IF EXISTS `Family`;
CREATE TABLE IF NOT EXISTS `Family` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`Role`	TEXT,
	`Responsable`	TEXT,
	`IdExploitation`	INTEGER,
	`Age`	INTEGER,
	`Sexe`	TEXT
);
DROP TABLE IF EXISTS `Externalites`;
CREATE TABLE IF NOT EXISTS `Externalites` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Type`	TEXT,
	`Nom`	TEXT,
	`IdSystUnit`	INTEGER,
	`IdDefCateg`	INTEGER,
	`Qte_av1`	NUMERIC,
	`Qte_1`	NUMERIC,
	`Notes`	TEXT,
	FOREIGN KEY(`IdDefCateg`) REFERENCES `Def_Categ`(`IdDefCateg`)
);
DROP TABLE IF EXISTS `Exploitation`;
CREATE TABLE IF NOT EXISTS `Exploitation` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdType`	INTEGER,
	`IdCaract_Exploi`	INTEGER,
	`EnCours`	INTEGER,
	`Principale`	INTEGER,
	`IdExploitationPrincipale`	INTEGER,
	`Notes`	TEXT,
	`IdProduits`	TEXT,
	FOREIGN KEY(`IdProduits`) REFERENCES `Produits`(`ID`),
	FOREIGN KEY(`IdType`) REFERENCES `Type_Exploi`(`ID`)
);
DROP TABLE IF EXISTS `Expense_Income`;
CREATE TABLE IF NOT EXISTS `Expense_Income` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdTVA`	INTEGER,
	`Expense`	INTEGER,
	`Family`	INTEGER,
	`Field6`	INTEGER,
	FOREIGN KEY(`IdTVA`) REFERENCES `TVA`(`IdTva`)
);
DROP TABLE IF EXISTS `EtatSortie`;
CREATE TABLE IF NOT EXISTS `EtatSortie` (
	`ID`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdDefinitions`	INTEGER,
	`IdDefCateg`	INTEGER,
	`IdDefEtatSortie`	INTEGER,
	`Item`	TEXT,
	`NoElt`	INTEGER,
	`Famille`	TEXT,
	`NoCateg`	TEXT,
	`NoItemElt`	TEXT,
	`Digits`	TEXT,
	`Couleur`	TEXT,
	`Titre`	TEXT
);
DROP TABLE IF EXISTS `Ensemble`;
CREATE TABLE IF NOT EXISTS `Ensemble` (
	`IdEnsemble`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`Notes`	TEXT
);
DROP TABLE IF EXISTS `Ens_Eff`;
CREATE TABLE IF NOT EXISTS `Ens_Eff` (
	`IdAgriEff`	INTEGER,
	`NoAgri`	INTEGER,
	`IdEnsemble`	INTEGER,
	`Nom`	TEXT,
	`N_X_1`	TEXT,
	`N_X_2`	TEXT,
	`N_X_3`	TEXT,
	`N_X_4`	TEXT,
	`N_X_5`	TEXT,
	`N_X_6`	TEXT,
	`N_X_7`	TEXT,
	`N_X_8`	TEXT,
	`N_X_9`	TEXT,
	`N_X_10`	TEXT,
	`IdEnsEff`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `ETHNIE`;
CREATE TABLE IF NOT EXISTS `ETHNIE` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`ID_SOCIO`	INTEGER,
	`ID_ETHNIE`	TEXT
);
DROP TABLE IF EXISTS `Def_SerieComp`;
CREATE TABLE IF NOT EXISTS `Def_SerieComp` (
	`IdDefinitions`	INTEGER,
	`IdDefCateg`	INTEGER,
	`IdEtatSortie`	INTEGER,
	`IdDefEtatSortie`	INTEGER,
	`Nom`	TEXT,
	`IdDefSerieComp`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Def_Item`;
CREATE TABLE IF NOT EXISTS `Def_Item` (
	`IdDefCateg`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdDefItem`	INTEGER,
	`Nom`	TEXT,
	`IdSysUnit`	INTEGER,
	`Prix`	TEXT,
	`IdTva`	INTEGER,
	PRIMARY KEY(`IdDefItem`)
);
DROP TABLE IF EXISTS `Def_Indicateur`;
CREATE TABLE IF NOT EXISTS `Def_Indicateur` (
	`IdDefCateg`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdDefItem`	INTEGER,
	`Nom`	TEXT,
	`Notes`	TEXT,
	`IdDefIndicateur`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
INSERT INTO `Def_Indicateur` (IdDefCateg,IdDefinitions,IdDefItem,Nom,Notes,IdDefIndicateur) VALUES ('',NULL,NULL,NULL,NULL,1);
DROP TABLE IF EXISTS `Def_EtatSortie`;
CREATE TABLE IF NOT EXISTS `Def_EtatSortie` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdDefinitions`	INTEGER,
	`IdDefCateg`	INTEGER,
	`IdEtatSortie`	INTEGER,
	`Nom`	TEXT
);
DROP TABLE IF EXISTS `Def_Dico`;
CREATE TABLE IF NOT EXISTS `Def_Dico` (
	`IdDefCateg`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdDefDico`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT
);
DROP TABLE IF EXISTS `Def_Critere`;
CREATE TABLE IF NOT EXISTS `Def_Critere` (
	`IdDefCateg`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdDefItem`	INTEGER,
	`Nom`	TEXT,
	`IdDefCritere`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Def_Conversion`;
CREATE TABLE IF NOT EXISTS `Def_Conversion` (
	`IdConversion`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdDefinitions`	INTEGER,
	`NoBase`	INTEGER,
	`NoSortie`	INTEGER,
	`APartirDe`	TEXT,
	`Nom`	TEXT,
	`EVF1`	TEXT,
	`EVF2`	TEXT,
	`EVF3`	TEXT,
	`EVF4`	TEXT,
	`EVF5`	TEXT,
	`EVF6`	TEXT,
	`EVF7`	TEXT,
	`EVF8`	TEXT,
	`EVF9`	TEXT,
	`EVF10`	TEXT,
	`FVE1`	TEXT,
	`FVE2`	TEXT,
	`FVE3`	TEXT,
	`FVE4`	TEXT,
	`FVE5`	TEXT,
	`FVE6`	TEXT,
	`FVE7`	TEXT,
	`FVE8`	TEXT,
	`FVE9`	TEXT,
	`FVE10`	TEXT
);
DROP TABLE IF EXISTS `Def_Categ`;
CREATE TABLE IF NOT EXISTS `Def_Categ` (
	`IdDefinitions`	INTEGER,
	`IdDefCateg`	INTEGER,
	`Nom`	TEXT,
	`Notes`	TEXT,
	PRIMARY KEY(`IdDefCateg`)
);
INSERT INTO `Def_Categ` (IdDefinitions,IdDefCateg,Nom,Notes) VALUES (1,1,'Céréales',''),
 (1,2,'Oléagineux',''),
 (1,3,'Protéagineux',''),
 (1,4,'pdt',''),
 (1,5,'prairies',''),
 (1,6,'jachère',''),
 (1,7,'autres',''),
 (1,8,'bett',''),
 (2,9,'Engrais',''),
 (2,10,'Semences',''),
 (2,11,'Phytosanitaires',''),
 (2,12,'autres',''),
 (3,13,'Positive',''),
 (3,14,'Négative',''),
 (6,15,'Charges structure',''),
 (13,16,'Bloc',''),
 (13,17,'simulation',''),
 (2,20,'Bovidés',NULL),
 (1,21,'Energie',NULL),
 (1,22,'Bovidés',NULL),
 (1,23,'Cacao',NULL),
 (2,24,'Essence',NULL),
 (2,25,'Eletricite',NULL);
DROP TABLE IF EXISTS `Def_Calendrier`;
CREATE TABLE IF NOT EXISTS `Def_Calendrier` (
	`IdDefCalendrier`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdDefCateg`	INTEGER,
	`Nom`	TEXT,
	`IdDefinitions`	INTEGER,
	`IdPeriode`	INTEGER,
	`Per_Act`	TEXT,
	`JDeb`	TEXT,
	`MDeb`	TEXT,
	`JFin`	TEXT,
	`MFin`	TEXT,
	`PcentDispo`	TEXT,
	`HpJ`	TEXT,
	`Jours`	REAL,
	`Jours Utilises`	REAL,
	`Heures_Utilisees`	REAL
);
DROP TABLE IF EXISTS `Def_Bestiaux`;
CREATE TABLE IF NOT EXISTS `Def_Bestiaux` (
	`IdDefCateg`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdBestiaux`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdOrigine`	INTEGER,
	`IdTva`	INTEGER,
	`Prix`	NUMERIC,
	`Vallnv`	TEXT,
	`Donne1`	TEXT,
	`DonnePcent1`	REAL,
	`Donne2`	TEXT,
	`DonnePcent2`	REAL,
	`Donne3`	TEXT,
	`DonnePcent3`	REAL,
	`Donne4`	TEXT,
	`DonnePcent4`	REAL,
	`Origine`	TEXT
);
DROP TABLE IF EXISTS `DICO_TYPO`;
CREATE TABLE IF NOT EXISTS `DICO_TYPO` (
	`ID`	TEXT,
	`TYPE`	TEXT
);
DROP TABLE IF EXISTS `DICO_PARTIE`;
CREATE TABLE IF NOT EXISTS `DICO_PARTIE` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`PARTIE`	TEXT,
	`field3`	TEXT
);
DROP TABLE IF EXISTS `DICO_NATURE`;
CREATE TABLE IF NOT EXISTS `DICO_NATURE` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`A_PRECISER`	TEXT,
	`NATURE`	TEXT
);
DROP TABLE IF EXISTS `Couleur`;
CREATE TABLE IF NOT EXISTS `Couleur` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Couleur1`	TEXT,
	`Couleur2`	TEXT,
	`Couleur3`	TEXT,
	`ARVB1`	TEXT,
	`ARVB2`	TEXT,
	`ARVB3`	TEXT
);
INSERT INTO `Couleur` (ID,Couleur1,Couleur2,Couleur3,ARVB1,ARVB2,ARVB3) VALUES (0,'ffffffb7','ff9dffff','0','-73','-6422529','0');
DROP TABLE IF EXISTS `Classifications`;
CREATE TABLE IF NOT EXISTS `Classifications` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Groupe`	TEXT,
	`Nom`	TEXT,
	`Notes`	TEXT
);
DROP TABLE IF EXISTS `Charges`;
CREATE TABLE IF NOT EXISTS `Charges` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdDefCateg`	INTEGER,
	`IdSysUnit`	INTEGER,
	`Prix`	TEXT,
	`IdTva`	INTEGER,
	`Structurelle`	INTEGER,
	`Notes`	TEXT,
	`IdQuantite`	INTEGER,
	`IdProduits`	INTEGER
);
DROP TABLE IF EXISTS `Charge_Quantite`;
CREATE TABLE IF NOT EXISTS `Charge_Quantite` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdCharges`	INTEGER,
	`Quantite_Avant_1`	REAL,
	`Quantite_1`	REAL,
	`IdActivite`	INTEGER
);
DROP TABLE IF EXISTS `Charge_Pluriannuelle`;
CREATE TABLE IF NOT EXISTS `Charge_Pluriannuelle` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Quantite_Avant_1`	TEXT,
	`_1`	TEXT,
	`_2`	TEXT,
	`_3`	TEXT,
	`_4`	TEXT,
	`IdActivite`	INTEGER,
	`IdCharges`	INTEGER
);
DROP TABLE IF EXISTS `Charge_Perenne`;
CREATE TABLE IF NOT EXISTS `Charge_Perenne` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdCharges`	INTEGER,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT,
	`Quantite_Avant_1`	TEXT,
	`IdActivite`	INTEGER
);DROP TABLE IF EXISTS `Caract_Exploitation`;
CREATE TABLE IF NOT EXISTS `Caract_Exploitation` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`IdType`	INTEGER,
	`IdCharges`	INTEGER,
	`IdPeriode`	INTEGER,
	`IdActivite`	INTEGER,
	`IdProduits`	INTEGER,
	`IdExternalites`	INTEGER,
	`NumVariante`	INTEGER,
	`IdFamily`	INTEGER
);
DROP TABLE IF EXISTS `Caract_Classifications`;
CREATE TABLE IF NOT EXISTS `Caract_Classifications` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`Classification_1`	TEXT,
	`Valeur_1`	TEXT,
	`Classification_2`	TEXT,
	`Valeur_2`	TEXT,
	`Classification_3`	TEXT,
	`Valeur_3`	TEXT,
	`Classification_4`	TEXT,
	`Valeur_4`	TEXT,
	`Classification_5`	TEXT,
	`Valeur_5`	TEXT,
	`Classification_6`	TEXT,
	`Valeur_6`	TEXT,
	`Classification_7`	TEXT,
	`Valeur_7`	TEXT,
	`Classification_8`	TEXT,
	`Valeur_8`	TEXT,
	`Classification_9`	TEXT,
	`Valeur_9`	TEXT,
	`Classification_10`	TEXT,
	`Valeur_10`	TEXT
);
DROP TABLE IF EXISTS `Caract_Activite`;
CREATE TABLE IF NOT EXISTS `Caract_Activite` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdActivite`	INTEGER,
	`IdType`	INTEGER,
	`IdProduits`	INTEGER,
	`IdCharges`	INTEGER,
	`Prod_Princ`	INTEGER,
	`IdExternalites`	INTEGER,
	`IdPeriodes`	INTEGER,
	`IDAvance`	INTEGER,
	`IdPiedHa`	INTEGER,
	`IdProdImmo`	INTEGER,
	`IdProdPied`	INTEGER,
	`IdChPied`	INTEGER,
	`IdQuantiteProd`	INTEGER
);
DROP TABLE IF EXISTS `CREATE_FC`;
CREATE TABLE IF NOT EXISTS `CREATE_FC` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`num_dossier`	INTEGER,
	`EJG`	TEXT,
	`ids_village`	TEXT,
	`prov`	TEXT,
	`dep`	TEXT,
	`canton`	TEXT,
	`contact`	TEXT,
	`date_depot`	TEXT,
	`date_accept`	TEXT,
	`map`	TEXT,
	`limite_nord`	TEXT,
	`limite_sud`	TEXT,
	`limite_est`	TEXT,
	`limite_ouest`	TEXT,
	`has_doc1`	TEXT,
	`has_doc2`	TEXT,
	`has_doc3`	TEXT,
	`has_doc4`	TEXT,
	`has_doc5`	TEXT,
	`has_doc6`	TEXT,
	`INPUT_DATE`	TEXT,
	`INPUT_ID`	TEXT,
	`surf`	TEXT,
	`num_convention`	TEXT,
	`remarque`	TEXT
);
DROP TABLE IF EXISTS `CLAN`;
CREATE TABLE IF NOT EXISTS `CLAN` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`ID_ETHNIE`	INTEGER,
	`NOM`	TEXT,
	`NOMBRE`	TEXT
);
DROP TABLE IF EXISTS `BIBLIO`;
CREATE TABLE IF NOT EXISTS `BIBLIO` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NOM`	TEXT,
	`DATE`	TEXT,
	`KEY`	TEXT
);
DROP TABLE IF EXISTS `Avance`;
CREATE TABLE IF NOT EXISTS `Avance` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Avant_1`	REAL,
	`_1`	REAL,
	`numero`	INTEGER,
	`IdActivite`	INTEGER
);
DROP TABLE IF EXISTS `Ate_PiedHa`;
CREATE TABLE IF NOT EXISTS `Ate_PiedHa` (
	`IdAteliers`	INTEGER,
	`IdAteCateg`	INTEGER,
	`IdAteSousCateg`	INTEGER,
	`IdAteItem`	INTEGER,
	`IdList`	INTEGER,
	`Ph0`	TEXT,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT,
	`IdAtePiedHa`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Ate_Item`;
CREATE TABLE IF NOT EXISTS `Ate_Item` (
	`IdAteliers`	INTEGER,
	`IdAteCateg`	INTEGER,
	`IdAteSousCateg`	INTEGER,
	`IdAteItem`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdList`	INTEGER,
	`C1IdDefinitions`	TEXT,
	`C1IdDefCateg`	TEXT,
	`C1IdDefItem`	TEXT,
	`C3IdDefinitions`	TEXT,
	`C3IdDefCateg`	TEXT,
	`C3IdDefItem`	TEXT,
	`Ph0`	TEXT,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT
);
DROP TABLE IF EXISTS `Ate_ImmoProd`;
CREATE TABLE IF NOT EXISTS `Ate_ImmoProd` (
	`IdAteliers`	INTEGER,
	`IdAteSousCateg`	INTEGER,
	`Item`	TEXT,
	`IdList`	INTEGER,
	`Ph0`	TEXT,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT,
	`IdAteImmoProd`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Ate_Categ`;
CREATE TABLE IF NOT EXISTS `Ate_Categ` (
	`IdAteliers`	INTEGER,
	`IdAteCateg`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`DebAmor`	TEXT,
	`DurAmor`	TEXT,
	`NbPhase`	TEXT
);
DROP TABLE IF EXISTS `Ate_CatPhase`;
CREATE TABLE IF NOT EXISTS `Ate_CatPhase` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdProduits`	INTEGER,
	`IdActivite`	INTEGER,
	`Ph1`	TEXT,
	`Ph2`	TEXT,
	`Ph3`	TEXT,
	`Ph4`	TEXT,
	`Ph5`	TEXT,
	`Ph6`	TEXT,
	`Ph7`	TEXT,
	`Ph8`	TEXT,
	`Ph9`	TEXT,
	`Ph10`	TEXT,
	`Ph11`	TEXT,
	`Ph12`	TEXT,
	`Ph13`	TEXT,
	`Ph14`	TEXT,
	`Ph15`	TEXT,
	`Ph16`	TEXT,
	`Ph17`	TEXT,
	`Ph18`	TEXT,
	`Ph19`	TEXT,
	`Ph20`	TEXT,
	`Ph21`	TEXT,
	`Ph22`	TEXT,
	`Ph23`	TEXT,
	`Ph24`	TEXT,
	`Ph25`	TEXT,
	`Ph26`	TEXT,
	`Ph27`	TEXT,
	`Ph28`	TEXT,
	`Ph29`	TEXT,
	`Ph30`	TEXT,
	`Ph31`	TEXT,
	`Ph32`	TEXT,
	`Ph33`	TEXT,
	`Ph34`	TEXT,
	`Ph35`	TEXT,
	`Ph36`	TEXT,
	`Ph37`	TEXT,
	`Ph38`	TEXT,
	`Ph39`	TEXT,
	`Ph40`	TEXT,
	`Max`	REAL,
	`Debut`	REAL
);
DROP TABLE IF EXISTS `Ate_BesTrav_Item`;
CREATE TABLE IF NOT EXISTS `Ate_BesTrav_Item` (
	`IdAteliers`	TEXT,
	`IdAteSousCateg`	TEXT,
	`IdDefCateg`	TEXT,
	`IdDefinitions`	TEXT,
	`iX`	TEXT,
	`jX`	TEXT,
	`Valeur`	TEXT
);
INSERT INTO `Ate_BesTrav_Item` (IdAteliers,IdAteSousCateg,IdDefCateg,IdDefinitions,iX,jX,Valeur) VALUES ('',NULL,NULL,NULL,NULL,NULL,NULL);
DROP TABLE IF EXISTS `Ate_BesTrav`;
CREATE TABLE IF NOT EXISTS `Ate_BesTrav` (
	`IdAteliers`	TEXT,
	`IdAteSousCateg`	TEXT,
	`IdDefCateg`	TEXT,
	`IdDefinitions`	TEXT,
	`NbPhase`	TEXT,
	`NbPeriodeCal`	TEXT
);
DROP TABLE IF EXISTS `Ate_Avance`;
CREATE TABLE IF NOT EXISTS `Ate_Avance` (
	`IdAteliers`	TEXT,
	`IdAteSousCateg`	TEXT,
	`Item`	TEXT,
	`Debut`	TEXT,
	`Fin`	TEXT,
	`Valeur`	TEXT
);
DROP TABLE IF EXISTS `Arbre`;
CREATE TABLE IF NOT EXISTS `Arbre` (
	`NoIndic`	TEXT,
	`nbrBranche`	TEXT,
	`noBranche`	TEXT,
	`code`	TEXT,
	`arg1`	TEXT,
	`arg2`	TEXT,
	`arg3`	TEXT,
	`valeur`	TEXT,
	`type`	TEXT,
	`dateBranche`	TEXT
);
DROP TABLE IF EXISTS `Alea_Item`;
CREATE TABLE IF NOT EXISTS `Alea_Item` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdProduits`	INTEGER,
	`IdCharges`	INTEGER,
	`IdAleaCateg`	INTEGER,
	`base`	TEXT,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT,
	`An11`	TEXT,
	`An12`	TEXT,
	`An13`	TEXT,
	`An14`	TEXT,
	`An15`	TEXT,
	`An16`	TEXT,
	`An17`	TEXT,
	`An18`	TEXT,
	`An19`	TEXT,
	`An20`	TEXT,
	`An21`	TEXT,
	`An22`	TEXT,
	`An23`	TEXT,
	`An24`	TEXT,
	`An25`	TEXT,
	`An26`	TEXT,
	`An27`	TEXT,
	`An28`	TEXT,
	`An29`	TEXT,
	`An30`	TEXT,
	`An31`	TEXT,
	`An32`	TEXT,
	`An33`	TEXT,
	`An34`	TEXT,
	`An35`	TEXT,
	`An36`	TEXT,
	`An37`	TEXT,
	`An38`	TEXT,
	`An39`	TEXT,
	`An40`	TEXT,
	`An41`	TEXT,
	`An42`	TEXT,
	`An43`	TEXT,
	`An44`	TEXT,
	`An45`	TEXT,
	`An46`	TEXT,
	`An47`	TEXT,
	`An48`	TEXT,
	`An49`	TEXT,
	`An50`	TEXT,
	`An51`	TEXT,
	`An52`	TEXT,
	`An53`	TEXT,
	`An54`	TEXT,
	`An55`	TEXT,
	`An56`	TEXT,
	`An57`	TEXT,
	`An58`	TEXT,
	`An59`	TEXT,
	`An60`	TEXT,
	`An61`	TEXT,
	`An62`	TEXT,
	`An63`	TEXT,
	`An64`	TEXT,
	`An65`	TEXT,
	`An66`	TEXT,
	`An67`	TEXT,
	`An68`	TEXT,
	`An69`	TEXT,
	`An70`	TEXT,
	`An71`	TEXT,
	`An72`	TEXT,
	`An73`	TEXT,
	`An74`	TEXT,
	`An75`	TEXT,
	`An76`	TEXT,
	`An77`	TEXT,
	`An78`	TEXT,
	`An79`	TEXT,
	`An80`	TEXT,
	`An81`	TEXT,
	`An82`	TEXT,
	`An83`	TEXT,
	`An84`	TEXT,
	`An85`	TEXT,
	`An86`	TEXT,
	`An87`	TEXT,
	`An88`	TEXT,
	`An89`	TEXT,
	`An90`	TEXT,
	`An91`	TEXT,
	`An92`	TEXT,
	`An93`	TEXT,
	`An94`	TEXT,
	`An95`	TEXT,
	`An96`	TEXT,
	`An97`	TEXT,
	`An98`	TEXT,
	`An99`	TEXT,
	`An100`	TEXT
);
INSERT INTO `Alea_Item` (ID,IdProduits,IdCharges,IdAleaCateg,base,An1,An2,An3,An4,An5,An6,An7,An8,An9,An10,An11,An12,An13,An14,An15,An16,An17,An18,An19,An20,An21,An22,An23,An24,An25,An26,An27,An28,An29,An30,An31,An32,An33,An34,An35,An36,An37,An38,An39,An40,An41,An42,An43,An44,An45,An46,An47,An48,An49,An50,An51,An52,An53,An54,An55,An56,An57,An58,An59,An60,An61,An62,An63,An64,An65,An66,An67,An68,An69,An70,An71,An72,An73,An74,An75,An76,An77,An78,An79,An80,An81,An82,An83,An84,An85,An86,An87,An88,An89,An90,An91,An92,An93,An94,An95,An96,An97,An98,An99,An100) VALUES (1,1,NULL,1,'blé','50','150','50','150','80','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100','100');
DROP TABLE IF EXISTS `Alea_Categ`;
CREATE TABLE IF NOT EXISTS `Alea_Categ` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdAleas`	INTEGER,
	`Nom`	TEXT,
	`Notes`	TEXT
);
INSERT INTO `Alea_Categ` (ID,IdAleas,Nom,Notes) VALUES (1,1,'ble',NULL);
DROP TABLE IF EXISTS `Agri_Vivrier`;
CREATE TABLE IF NOT EXISTS `Agri_Vivrier` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`IdAteliers`	INTEGER,
	`NoAteSousCateg`	INTEGER,
	`AnPlant`	TEXT,
	`Duree`	TEXT,
	`Surface`	TEXT,
	`IdAgriVivier`	INTEGER PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_Variable`;
CREATE TABLE IF NOT EXISTS `Agri_Variable` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`IdVariable`	INTEGER,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT
);
DROP TABLE IF EXISTS `Agri_Sub`;
CREATE TABLE IF NOT EXISTS `Agri_Sub` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`Nom`	TEXT,
	`Montant`	TEXT,
	`DateReal`	TEXT,
	`Duree`	TEXT
);
DROP TABLE IF EXISTS `Agri_StockIni`;
CREATE TABLE IF NOT EXISTS `Agri_StockIni` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`IdProduit`	INTEGER,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT,
	`Prix`	INTEGER
);
DROP TABLE IF EXISTS `Agri_RepartItem`;
CREATE TABLE IF NOT EXISTS `Agri_RepartItem` (
	`IdRepartItem`	INTEGER,
	`NoAgri`	INTEGER,
	`NoFamille`	INTEGER,
	`Nox`	TEXT,
	`Nb`	INTEGER,
	`NoRepartQ1`	TEXT,
	`NoRepartV1`	TEXT,
	`NoRepartQ2`	TEXT,
	`NoRepartV2`	TEXT,
	`NoRepartQ3`	TEXT,
	`NoRepartV3`	TEXT,
	`NoRepartQ4`	TEXT,
	`NoRepartV4`	TEXT,
	`NoRepartQ5`	TEXT,
	`NoRepartV5`	TEXT,
	`NoRepartQ6`	TEXT,
	`NoRepartV6`	TEXT,
	`NoRepartQ7`	TEXT,
	`NoRepartV7`	TEXT,
	`NoRepartQ8`	TEXT,
	`NoRepartV8`	TEXT,
	`NoRepartQ9`	TEXT,
	`NoRepartV9`	TEXT,
	`NoRepartQ10`	TEXT,
	`NoRepartV10`	TEXT,
	`IdAgriRepartItem`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_Produits`;
CREATE TABLE IF NOT EXISTS `Agri_Produits` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdProduits`	INTEGER,
	`Formule`	TEXT,
	`IdExploitations`	INTEGER,
	FOREIGN KEY(`IdProduits`) REFERENCES `Produits`(`ID`),
	FOREIGN KEY(`IdExploitations`) REFERENCES `Exploitation`(`ID`)
);
DROP TABLE IF EXISTS `Agri_PolStock`;
CREATE TABLE IF NOT EXISTS `Agri_PolStock` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`Prix`	TEXT,
	`IdSysUnit`	INTEGER,
	`PcentStock`	TEXT,
	`MaxiStockable`	TEXT,
	`PcentDecote`	TEXT,
	`IdAgriPolStock`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_Pluriannuelle`;
CREATE TABLE IF NOT EXISTS `Agri_Pluriannuelle` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitations`	INTEGER,
	`IdActivite`	INTEGER,
	`NoItem`	INTEGER,
	`NoAteSousCateg`	INTEGER,
	`AnPlant`	TEXT,
	`AnArr`	TEXT,
	`Surface`	TEXT
);
DROP TABLE IF EXISTS `Agri_Plact`;
CREATE TABLE IF NOT EXISTS `Agri_Plact` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`NoItem`	INTEGER,
	`Nom`	TEXT,
	`Montant`	TEXT,
	`Taux`	TEXT,
	`Type`	TEXT,
	`DateReal`	TEXT,
	`DateTerme`	TEXT,
	`Ent_Pri`	TEXT
);
DROP TABLE IF EXISTS `Agri_Petit`;
CREATE TABLE IF NOT EXISTS `Agri_Petit` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`Année`	TEXT,
	`ValAchat`	TEXT,
	`NoTva`	INTEGER,
	`IdAgriPetit`	INTEGER
);
DROP TABLE IF EXISTS `Agri_Perenne`;
CREATE TABLE IF NOT EXISTS `Agri_Perenne` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitations`	INTEGER,
	`IdActivite`	INTEGER,
	`NoItem`	INTEGER,
	`NoAteSousCateg`	INTEGER,
	`AnPlant`	TEXT,
	`AnArr`	TEXT,
	`Surface`	TEXT
);
DROP TABLE IF EXISTS `Agri_Occc`;
CREATE TABLE IF NOT EXISTS `Agri_Occc` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`Année`	INTEGER,
	`Montant`	REAL,
	`Taux`	REAL,
	`Pcent`	REAL,
	`Frais`	REAL
);
DROP TABLE IF EXISTS `Agri_ImmoGlobal`;
CREATE TABLE IF NOT EXISTS `Agri_ImmoGlobal` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`NoItem`	INTEGER,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT
);
DROP TABLE IF EXISTS `Agri_Immo`;
CREATE TABLE IF NOT EXISTS `Agri_Immo` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`Nom`	TEXT,
	`NoTva`	INTEGER,
	`ValAchat`	TEXT,
	`AAchat`	TEXT,
	`MAchat`	TEXT,
	`AVente`	TEXT,
	`MVente`	TEXT,
	`TypeAmor`	TEXT,
	`Duree`	TEXT,
	`ValVente`	TEXT,
	`IdAgriImmo`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_FormuleReseau`;
CREATE TABLE IF NOT EXISTS `Agri_FormuleReseau` (
	`NoAgri`	INTEGER,
	`NoFormule`	INTEGER,
	`NoItem`	INTEGER,
	`IdList`	INTEGER,
	`NoAn`	INTEGER,
	`nbrMaille`	NUMERIC,
	`NoMaille`	NUMERIC,
	`codeReseau`	TEXT,
	`codeBranche`	TEXT,
	`apDreseau`	TEXT,
	`apGreseau`	TEXT,
	`AgriFormuleReseau`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_FormuleArbre`;
CREATE TABLE IF NOT EXISTS `Agri_FormuleArbre` (
	`NoAgri`	INTEGER,
	`NoFormule`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdList`	INTEGER,
	`NoItem`	INTEGER,
	`NoAn`	INTEGER,
	`nbrBranche`	INTEGER,
	`noBranche`	NUMERIC,
	`code`	TEXT,
	`arg1`	TEXT,
	`arg2`	TEXT,
	`arg3`	TEXT,
	`valeur`	TEXT,
	`type`	TEXT,
	`dateBranche`	TEXT,
	`IdGriFormuleArbre`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_Formule`;
CREATE TABLE IF NOT EXISTS `Agri_Formule` (
	`NoAgri`	INTEGER,
	`NoFormule`	INTEGER,
	`IdDefinitions`	INTEGER,
	`IdList`	INTEGER,
	`NoItem`	INTEGER,
	`Origine`	TEXT,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT,
	`IdAgriFormule`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_FinanceGlobal`;
CREATE TABLE IF NOT EXISTS `Agri_FinanceGlobal` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT,
	`IdAgriFinanceGlobal`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_Externalites`;
CREATE TABLE IF NOT EXISTS `Agri_Externalites` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExternalites`	INTEGER,
	`Formule`	TEXT,
	`IdExploitations`	INTEGER,
	FOREIGN KEY(`IdExternalites`) REFERENCES `Externalites`(`ID`),
	FOREIGN KEY(`IdExploitations`) REFERENCES `Exploitation`(`ID`)
);
DROP TABLE IF EXISTS `Agri_EnCoursDette`;
CREATE TABLE IF NOT EXISTS `Agri_EnCoursDette` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`Valeur`	TEXT,
	`MPaie`	TEXT,
	`APaie`	TEXT,
	`NoFamille`	INTEGER,
	`IdAgriEncourDette`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_EnCoursCreance`;
CREATE TABLE IF NOT EXISTS `Agri_EnCoursCreance` (
	`NoAgri`	INTEGER,
	`NoItem`	INTEGER,
	`Valeur`	TEXT,
	`MPaie`	TEXT,
	`APaie`	TEXT,
	`NoFamille`	INTEGER,
	`IdAgriEnCoursCre`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_EmpLT`;
CREATE TABLE IF NOT EXISTS `Agri_EmpLT` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`Nom`	TEXT,
	`Montant`	TEXT,
	`Duree1`	TEXT,
	`Duree2`	TEXT,
	`Taux1`	REAL,
	`Taux2`	REAL,
	`Type`	TEXT,
	`DateReal`	TEXT,
	`DateRemb`	TEXT,
	`Ent_Pri`	TEXT,
	`Nombre_Variation`	INTEGER,
	`Duree3`	TEXT,
	`Duree4`	TEXT,
	`Duree5`	TEXT,
	`Taux3`	REAL,
	`Taux4`	REAL,
	`Taux5`	REAL,
	`Periodicite`	TEXT
);
DROP TABLE IF EXISTS `Agri_EmpCT`;
CREATE TABLE IF NOT EXISTS `Agri_EmpCT` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitation`	INTEGER,
	`Nom`	TEXT,
	`Montant`	TEXT,
	`Taux`	NUMERIC,
	`Type`	TEXT,
	`DateReal`	TEXT,
	`DateRemb`	TEXT,
	`Ent_Pri`	TEXT
);
INSERT INTO `Agri_EmpCT` (ID,IdExploitation,Nom,Montant,Taux,Type,DateReal,DateRemb,Ent_Pri) VALUES (1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
DROP TABLE IF EXISTS `Agri_DefSim`;
CREATE TABLE IF NOT EXISTS `Agri_DefSim` (
	`IdAgriDefSim`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitations`	INTEGER,
	`An_0`	TEXT,
	`NbAnSim`	INTEGER,
	`MDeb`	TEXT,
	`AnCalage`	INTEGER,
	`TypeAlea`	TEXT,
	`NoPrixProd`	INTEGER,
	`NoPrixCharge`	INTEGER,
	`NoQProd`	INTEGER,
	`NoQCharge`	INTEGER,
	`NoQExt`	INTEGER,
	`Notes`	INTEGER
);
DROP TABLE IF EXISTS `Agri_Critere`;
CREATE TABLE IF NOT EXISTS `Agri_Critere` (
	`NoAgri`	INTEGER NOT NULL,
	`NoCritere`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
DROP TABLE IF EXISTS `Agri_Charges`;
CREATE TABLE IF NOT EXISTS `Agri_Charges` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdCharges`	INTEGER,
	`IdExploitations`	INTEGER,
	`Formule`	TEXT,
	FOREIGN KEY(`IdExploitations`) REFERENCES `Exploitation`(`ID`),
	FOREIGN KEY(`IdCharges`) REFERENCES `Charges`(`ID`)
);
DROP TABLE IF EXISTS `Agri_Assol`;
CREATE TABLE IF NOT EXISTS `Agri_Assol` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitations`	INTEGER,
	`IdActivite`	INTEGER,
	`An1`	TEXT,
	`An2`	TEXT,
	`An3`	TEXT,
	`An4`	TEXT,
	`An5`	TEXT,
	`An6`	TEXT,
	`An7`	TEXT,
	`An8`	TEXT,
	`An9`	TEXT,
	`An10`	TEXT,
	FOREIGN KEY(`IdExploitations`) REFERENCES `Exploitation`(`ID`),
	FOREIGN KEY(`IdActivite`) REFERENCES `Activite`(`ID`)
);
DROP TABLE IF EXISTS `Agri_Animaux`;
CREATE TABLE IF NOT EXISTS `Agri_Animaux` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`IdExploitations`	INTEGER,
	`IdActivite`	INTEGER
);
DROP TABLE IF EXISTS `Agri`;
CREATE TABLE IF NOT EXISTS `Agri` (
	`IdAgri`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`NoVariante`	TEXT,
	`Nom`	TEXT,
	`OrVar`	TEXT,
	`OrSerie`	TEXT,
	`NoSerie`	TEXT,
	`NbAnSim`	TEXT,
	`An_0`	TEXT,
	`Notes`	TEXT
);
DROP TABLE IF EXISTS `Activite`;
CREATE TABLE IF NOT EXISTS `Activite` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Nom`	TEXT,
	`IdType`	INTEGER,
	`Id_Info_GPS`	INTEGER,
	`IdCarat_Activite`	INTEGER,
	`Notes`	TEXT,
	`Encours`	INTEGER,
	`Culture_princ`	INTEGER,
	FOREIGN KEY(`IdType`) REFERENCES `Type`(`ID`)
);
DROP VIEW IF EXISTS `V_Activite`;
CREATE VIEW V_Activite as
SELECT 
Activite.Nom,
Activite.IdProd_Princ as produit ,
Activite.IdCharges as charges,
Activite.IdPeriode as periode,
Activite.IdType as _type
from Activite
inner join produit on Def_Item.IdDefItem = Activite.IdProd_Princ
inner join charges on Def_Item.IdDefItem = Activite.IdCharges
inner join peridode on Def_Calendrier.IdDefCalendrier = Activite.IdPeriode
inner join _type on Type.ID = Activite.IdType;
COMMIT;
