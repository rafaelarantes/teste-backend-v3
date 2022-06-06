﻿namespace TheatricalPlayersRefactoringKata.Performances
{
    public class TragedyPlay : Play
    {
        private const int TRAGEDY_ADICIONAL_AUDIENCE_VALUE = 10;
        private const int TRAGEDY_MAX_AUDIENCE = 30;

        public TragedyPlay(string name, int lines) : base(name, lines)
        {
        }

        public override void CalculateBaseValue(int audience)
        {
            if (audience > TRAGEDY_MAX_AUDIENCE)
                SumBaseValue(TRAGEDY_ADICIONAL_AUDIENCE_VALUE * (audience - TRAGEDY_MAX_AUDIENCE));
        }

        protected override int CalculateCredits(int audience)
        {
            return 0;
        }
    }
}