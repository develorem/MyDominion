import React, { useState, useEffect } from 'react';
import { Card, CardContent, CardMedia } from '@material-ui/core';
import CardModel from '../models/card'

interface Props {
    card: CardModel
}

const CardComponent = (props:Props) => {    
    const imagePath = props.card.LocalImageFileName.replace(/\\/g, "/");
    const path = `/carddata${imagePath}`;
    
    return (
        
        <img src={path} style={{height:'auto', width: 200}} />
        
    )
}

export default CardComponent;