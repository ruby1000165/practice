/*�Ĥ@�D:�C�ӭɾ\�H�C�~�ɮѼƶq�A�ḙ̀ɾ\�H�s���M�~�װ��Ƨ�*/
SELECT DISTINCT USER_ID AS KeeperId, USER_CNAME AS CName, USER_ENAME AS EName, 
       YEAR(LEND_DATE) AS BorrowYear,COUNT(KEEPER_ID) AS BorrowCnt
FROM   BOOK_LEND_RECORD INNER JOIN MEMBER_M 
	   ON  USER_ID = KEEPER_ID 
GROUP BY  USER_ID,USER_CNAME,USER_ENAME,YEAR(LEND_DATE)
ORDER BY  KeeperId,BorrowYear  

/*�ĤG�D:�C�X�̨��w�諸�ѫe���W(�ɾ\�ƶq�̦h�e���W)*/
SELECT TOP (5) WITH TIES BOOK_LEND_RECORD.BOOK_ID AS BookId, 
       BOOK_DATA.BOOK_NAME AS BookName, COUNT(BOOK_LEND_RECORD.BOOK_ID) AS QTY 
FROM   BOOK_LEND_RECORD INNER JOIN BOOK_DATA  
	  ON BOOK_LEND_RECORD.BOOK_ID = BOOK_DATA.BOOK_ID
GROUP BY BOOK_LEND_RECORD.BOOK_ID,BOOK_DATA.BOOK_NAME
ORDER BY QTY DESC 
 
/*�ĤT�D:�H�@�u�C�X2019�~�C�@�u���y�ɾ\�Ѷq */
SELECT  DISTINCT CASE WHEN MONTH(LEND_DATE) = '01' THEN '2019/01~2019/03 ' 
		WHEN MONTH(LEND_DATE) = '02'  THEN '2019/01~2019/03 '
		WHEN MONTH(LEND_DATE) = '03'  THEN '2019/01~2019/03 '
		WHEN MONTH(LEND_DATE) = '04'  THEN '2019/04~2019/06 '
		WHEN MONTH(LEND_DATE) = '05'  THEN '2019/04~2019/06 '
		WHEN MONTH(LEND_DATE) = '06'  THEN '2019/04~2019/06 '
		WHEN MONTH(LEND_DATE) = '07'  THEN '2019/07~2019/09 '
		WHEN MONTH(LEND_DATE) = '08'  THEN '2019/07~2019/09 '
		WHEN MONTH(LEND_DATE) = '09'  THEN '2019/07~2019/09 '
		WHEN MONTH(LEND_DATE) = '10'  THEN '2019/10~2019/12 '
		WHEN MONTH(LEND_DATE) = '11'  THEN '2019/10~2019/12 '
		WHEN MONTH(LEND_DATE) = '12'  THEN '2019/10~2019/12 ' 
		END AS Quarter, COUNT(MONTH(LEND_DATE)) AS Cnt 
FROM   BOOK_LEND_RECORD 
WHERE  LEND_DATE >= '2019-01-01 00:00:00.000' AND LEND_DATE <= '2019-12-31 00:00:00'
GROUP BY  CASE WHEN MONTH(LEND_DATE) = '01' THEN '2019/01~2019/03 ' 
		WHEN MONTH(LEND_DATE) = '02'  THEN '2019/01~2019/03 '
		WHEN MONTH(LEND_DATE) = '03'  THEN '2019/01~2019/03 '
		WHEN MONTH(LEND_DATE) = '04'  THEN '2019/04~2019/06 '
		WHEN MONTH(LEND_DATE) = '05'  THEN '2019/04~2019/06 '
		WHEN MONTH(LEND_DATE) = '06'  THEN '2019/04~2019/06 '
		WHEN MONTH(LEND_DATE) = '07'  THEN '2019/07~2019/09 '
		WHEN MONTH(LEND_DATE) = '08'  THEN '2019/07~2019/09 '
		WHEN MONTH(LEND_DATE) = '09'  THEN '2019/07~2019/09 '
		WHEN MONTH(LEND_DATE) = '10'  THEN '2019/10~2019/12 '
		WHEN MONTH(LEND_DATE) = '11'  THEN '2019/10~2019/12 '
		WHEN MONTH(LEND_DATE) = '12'  THEN '2019/10~2019/12 ' 
		END

/*�ĥ|�D:���X�C�Ӥ����ɾ\�ƶq�e�T�W�ѥ��μƶq*/
SELECT  *
FROM (SELECT ROW_NUMBER()OVER(PARTITION BY BOOK_CLASS_NAME ORDER BY COUNT(KEEPER_ID) DESC)AS Seq,
		BOOK_CLASS_NAME AS BookClass,BOOK_LEND_RECORD.BOOK_ID AS BookId,BOOK_NAME AS BookName,COUNT(KEEPER_ID) AS Cnt
     FROM BOOK_LEND_RECORD 
     JOIN BOOK_DATA ON BOOK_LEND_RECORD.BOOK_ID=BOOK_DATA.BOOK_ID
	 JOIN BOOK_CLASS ON BOOK_CLASS.BOOK_CLASS_ID=BOOK_DATA.BOOK_CLASS_ID
     GROUP BY BOOK_CLASS_NAME,BOOK_LEND_RECORD.BOOK_ID,BOOK_NAME
) AS T1
WHERE T1.Seq <= '3'
ORDER BY BookClass,Cnt DESC,BookId  --����ĳ�b�l�͸�ƪ�����ORDER BY

/*�Ĥ��D:�ЦC�X 2016, 2017, 2018, 2019 �U���y���O���ɾ\�ƶq���*/
SELECT DISTINCT BOOK_CLASS.BOOK_CLASS_ID AS ClassId, BOOK_CLASS_NAME AS ClassName, 
	   COUNT(CASE WHEN (YEAR(LEND_DATE)=2016) THEN 1 END) AS CNT2016, 
	   COUNT(CASE WHEN (YEAR(LEND_DATE)=2017) THEN 2 END) AS CNT2017,
	   COUNT(CASE WHEN (YEAR(LEND_DATE)=2018) THEN 3 END) AS CNT2018,
	   COUNT(CASE WHEN (YEAR(LEND_DATE)=2019) THEN 4 END) AS CNT2019
FROM  BOOK_DATA 
      JOIN BOOK_LEND_RECORD ON BOOK_DATA.BOOK_ID = BOOK_LEND_RECORD.BOOK_ID 
	  JOIN BOOK_CLASS ON BOOK_CLASS.BOOK_CLASS_ID = BOOK_DATA.BOOK_CLASS_ID
GROUP BY BOOK_CLASS.BOOK_CLASS_ID, BOOK_CLASS_NAME
ORDER BY ClassId

/*�Ĥ��D:�Шϥ� PIVOT �y�k�C�X2016, 2017, 2018, 2019 �U���y���O���ɾ\�ƶq���*/
SELECT ClassId,ClassName,ISNULL([2016],0) AS [CNT2016],ISNULL([2017],0) AS [CNT2017],
       ISNULL([2018],0) AS [CNT2018],ISNULL([2019],0) AS [CNT2019] 
FROM  (SELECT BOOK_CLASS.BOOK_CLASS_ID AS ClassId, BOOK_CLASS_NAME AS ClassName,
              YEAR(LEND_DATE) AS DAT,COUNT(KEEPER_ID) AS CNT
        FROM BOOK_DATA
             JOIN BOOK_LEND_RECORD ON BOOK_DATA.BOOK_ID = BOOK_LEND_RECORD.BOOK_ID 
	         JOIN BOOK_CLASS ON BOOK_CLASS.BOOK_CLASS_ID = BOOK_DATA.BOOK_CLASS_ID
		GROUP BY BOOK_CLASS.BOOK_CLASS_ID, BOOK_CLASS_NAME,YEAR(LEND_DATE)) AS G
PIVOT(SUM(CNT) FOR G.DAT
      IN([2016],[2017],[2018],[2019])
	  ) AS pvt
ORDER BY ClassId

/*�ĤC�D:�Ьd�ߥX���|���ɮѬ���*/
SELECT DISTINCT BOOK_LEND_RECORD.BOOK_ID AS [�ѥ�ID], 
       (CONVERT(VARCHAR(100), BOOK_BOUGHT_DATE,111)) AS [�ʮѤ��], 
       (CONVERT(VARCHAR(100),LEND_DATE,111)) AS [�ɾ\���], 
	   (BOOK_CLASS.BOOK_CLASS_ID + '-' + BOOK_CLASS_NAME) AS [���y���O],
	   (KEEPER_ID + '-' + USER_CNAME + '(' + USER_ENAME + ')') AS [�ɾ\�H], 
	   (CODE_ID + '-' + CODE_NAME) AS [���A], 
	   (PARSENAME(CONVERT(VARCHAR,CONVERT(MONEY,BOOK_AMOUNT),1),2) + '��') AS [�ʮѪ��B]
FROM   BOOK_DATA
	   JOIN BOOK_LEND_RECORD ON BOOK_DATA.BOOK_ID = BOOK_LEND_RECORD.BOOK_ID 
	   JOIN BOOK_CLASS ON BOOK_CLASS.BOOK_CLASS_ID = BOOK_DATA.BOOK_CLASS_ID
	   JOIN BOOK_CODE ON BOOK_CODE.CODE_ID = BOOK_STATUS
	   JOIN MEMBER_M  ON BOOK_LEND_RECORD.KEEPER_ID = MEMBER_M.USER_ID
WHERE  MEMBER_M.USER_CNAME = '���|' 
ORDER BY �ѥ�ID DESC

/*�ĤK�D:�s�W�@���ɾ\�����A�ɮѤH�����|�A�ѥ�ID��2004�A�íק�ɾ\�����2019/01/02*/
INSERT INTO BOOK_LEND_RECORD (BOOK_ID,KEEPER_ID,LEND_DATE)
VALUES (2004,'0002','2018-01-01 00:00:00.000')

SELECT DISTINCT BOOK_LEND_RECORD.BOOK_ID AS [�ѥ�ID], 
       (CONVERT(VARCHAR(100), BOOK_BOUGHT_DATE,111)) AS [�ʮѤ��], 
	   (CONVERT(VARCHAR(100),LEND_DATE,111)) AS [�ɾ\���], 
	   (BOOK_CLASS.BOOK_CLASS_ID + '-' + BOOK_CLASS_NAME) AS [���y���O],
	   (KEEPER_ID + '-' + USER_CNAME + '(' + USER_ENAME + ')') AS [�ɾ\�H], 
	   (CODE_ID + '-' + CODE_NAME) AS [���A], 
	   (PARSENAME(CONVERT(VARCHAR,CONVERT(MONEY,BOOK_AMOUNT),1),2) + '��') AS [�ʮѪ��B]
FROM   BOOK_DATA
	   JOIN BOOK_LEND_RECORD ON BOOK_DATA.BOOK_ID = BOOK_LEND_RECORD.BOOK_ID 
	   JOIN BOOK_CLASS ON BOOK_CLASS.BOOK_CLASS_ID = BOOK_DATA.BOOK_CLASS_ID
	   JOIN BOOK_CODE ON BOOK_CODE.CODE_ID = BOOK_STATUS
	   JOIN MEMBER_M  ON BOOK_LEND_RECORD.KEEPER_ID = MEMBER_M.USER_ID
WHERE  MEMBER_M.USER_CNAME = '���|'
ORDER BY �ѥ�ID DESC

UPDATE BOOK_LEND_RECORD
SET    LEND_DATE = '2019-01-02 00:00:00.000'
WHERE  BOOK_ID = 2004
       AND LEND_DATE = '2018-01-01 00:00:00.000'

/*�ĤE�D:�бN�D8�s�W���ɾ\����(�ѥ�ID=2004)�R��*/
DELETE FROM BOOK_LEND_RECORD
WHERE BOOK_ID = 2004
      AND LEND_DATE = '2019-01-02 00:00:00.000'

SELECT DISTINCT BOOK_LEND_RECORD.BOOK_ID AS [�ѥ�ID],
       (CONVERT(VARCHAR(100), BOOK_BOUGHT_DATE,111)) AS [�ʮѤ��], 
	   (CONVERT(VARCHAR(100),LEND_DATE,111)) AS [�ɾ\���], 
	   (BOOK_CLASS.BOOK_CLASS_ID + '-' + BOOK_CLASS_NAME) AS [���y���O],
	   (KEEPER_ID + '-' + USER_CNAME + '(' + USER_ENAME + ')') AS [�ɾ\�H], 
	   (CODE_ID + '-' + CODE_NAME) AS [���A], 
	   (PARSENAME(CONVERT(VARCHAR,CONVERT(MONEY,BOOK_AMOUNT),1),2) + '��') AS [�ʮѪ��B]
FROM   BOOK_DATA
	   JOIN BOOK_LEND_RECORD ON BOOK_DATA.BOOK_ID = BOOK_LEND_RECORD.BOOK_ID 
	   JOIN BOOK_CLASS ON BOOK_CLASS.BOOK_CLASS_ID = BOOK_DATA.BOOK_CLASS_ID
	   JOIN BOOK_CODE ON BOOK_CODE.CODE_ID = BOOK_STATUS
	   JOIN MEMBER_M  ON BOOK_LEND_RECORD.KEEPER_ID = MEMBER_M.USER_ID
WHERE  MEMBER_M.USER_CNAME = '���|'
ORDER BY �ѥ�ID DESC




 


