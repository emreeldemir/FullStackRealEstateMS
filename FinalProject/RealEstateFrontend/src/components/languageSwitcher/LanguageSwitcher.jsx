import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import './languageSwitcher.scss';

function LanguageSwitcher() {
    const { t, i18n } = useTranslation();
    const [isOpen, setIsOpen] = useState(false);

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
        setIsOpen(false);
    };

    const toggleDropdown = () => {
        setIsOpen(!isOpen);
    };

    return (
        <div className="language-switcher">
            <button onClick={toggleDropdown} className="dropdown-button">
                {t('language')}
            </button>
            {isOpen && (
                <div className="dropdown-menu">
                    <button onClick={() => changeLanguage('en')}>English (US)</button>
                    <button onClick={() => changeLanguage('tr')}>Türkçe (TR)</button>
                </div>
            )}
        </div>
    );
}

export default LanguageSwitcher;
