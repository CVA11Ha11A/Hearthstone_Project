using System;


public interface IDeckFunction
{
    // ! 덱에 있는 컬렉션[] 에 카드를 넣어주는 기능 제작해야함
    public void AddCardInDeck(CardID addCardId_);
    #region AddCardInDeck 가이드
    // 가이드 
    //if (currentIndex == MAXCOUNT - 1)
    //{
    //    return;
    //}
    //
    //cardList[currentIndex] = addCard_;
    //currentIndex++;
    //count++;
    #endregion


    // 해당 덱의 영웅 설정 하는 함수
    public void SetDeckClass(ClassCard deckHero_);
    #region SetDeckClass가이드
    // this.deckCalss = heroClass_;
    #endregion
}       // interface End
