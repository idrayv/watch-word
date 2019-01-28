export enum MaterialMode {
  Read, Edit, Add
}

export class MaterialStats {
  public name: string;
  public value: string;
}

export class VocabWordFiltration {
  private _showLearnWords: boolean;
  private _showKnownWords: boolean;
  private _showUnsignedWords: boolean;

  constructor(showLearnWords = true, showKnownWords = true, showUnsignedWords = true) {
    this._showLearnWords = showLearnWords;
    this._showKnownWords = showKnownWords;
    this._showUnsignedWords = showUnsignedWords;
  }

  public get showLearnWords(): boolean {
    return this._showLearnWords;
  }

  public get showKnownWords(): boolean {
    return this._showKnownWords;
  }

  public get showUnsignedWords(): boolean {
    return this._showUnsignedWords;
  }

  public invertLearnWord(): VocabWordFiltration {
    const result = this.clone();
    result._showLearnWords = !this._showLearnWords;
    return result;
  }

  public invertKnownWord(): VocabWordFiltration {
    const result = this.clone();
    result._showKnownWords = !this._showKnownWords;
    return result;
  }

  public invertUnsignedWord(): VocabWordFiltration {
    const result = this.clone();
    result._showUnsignedWords = !this._showUnsignedWords;
    return result;
  }

  private clone(): VocabWordFiltration {
    return new VocabWordFiltration(this._showLearnWords, this._showKnownWords, this._showUnsignedWords);
  }
}
