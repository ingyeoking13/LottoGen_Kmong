# Readme.md 

1 엑셀 쿼리를 활용한 조합 필터링  LottoGen_Kmong

2 엑셀을 활용한 갯수 카운팅 LottoCheck_Kmong

유사한 프로젝트들이 진행되면서 얼마나 재사용성이 좋은 코드를 짜는가,  
코어한 것들은 개념적으로 어떻게 정리되어야하는가,  
예외처리는 적절한 위치에 잘 되고 있는가  

1->2 로 넘어오면서 사용할 수 있었던 것들

`Helper`, `Services` 동일하게 사용. (뿌듯)    

`Model`, `ExcelWrapper` 일부 수정해야했음. (고민)  
`Excel Accessor` - `AbstractExcelRead/Writer <T>`  - `ExcelbyteReader/byteWriter` 로 더 추상화 해줌 .   

`LottoLogic` 심각. 왜 1에서 그땐 `coreLogic`이라는 표현을 썼느지 잘모르겠다.  
그래도 개념적으로 비슷한 것이 있었기에, IEnumerable/ICollection 을 중첩해서 순회하는 부분은 살릴 수 있었다.   

예외처리는 여전히 산재 ...  

