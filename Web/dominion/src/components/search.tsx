import React, { useState, useEffect } from 'react';
import { Checkbox, FormControlLabel, Grid, List, ListItem, Divider, Paper } from '@material-ui/core';
import { Set, Card } from '../models';
import { getCardData } from '../services/carddataimporter';
import CardComponent from './card';

const SearchComponent = () => {

    const [ sets, setSets ] = useState<Set[]>([]);
    const [ selectedSets, setSelectedSets ] = useState<string[]>([]);
    const [ selectedCardTypes, setSelectedCardTypes ] = useState<string[]>([]);

    useEffect(() => {
        const setData = getCardData() 
        setSets(setData);
    }, []);

    const onHandleSelectedSetChanged = (event: { target: { name: string; }; }) => {
        const name = event.target.name;
        const newSelectedSets  = Object.assign([], selectedSets);
        console.log(newSelectedSets);
        const index = newSelectedSets.indexOf(name, 0);
        if (index > -1) {
            newSelectedSets.splice(index, 1);
        }
        else {
            newSelectedSets.push(name);
        }
        setSelectedSets(newSelectedSets);
    }

    const onHandleSelectedCardTypeChanged = (event: { target: { name: string; }; }) => {
        const name = event.target.name;
        const newCardTypes  = Object.assign([], selectedCardTypes);
        console.log(newCardTypes);
        const index = newCardTypes.indexOf(name, 0);
        if (index > -1) {
            newCardTypes.splice(index, 1);
        }
        else {
            newCardTypes.push(name);
        }
        setSelectedCardTypes(newCardTypes);
    }

    const renderSetCheckboxes = () => {
        if (sets && sets.length > 0) {
            return (
                sets.map(s => 
                    <ListItem>
                  <FormControlLabel key={s.Name} control={
                        <Checkbox
                            key={s.Name}
                            name={s.Name}                                                
                            color="primary"
                            checked={selectedSets.includes(s.Name)}
                            onChange={onHandleSelectedSetChanged}
                        />
                        }
                        label={s.Name}
                    />        
                    </ListItem>     
                )
            )
        }

        return (
            <div>Sets don't seem to have loaded. Better debug.</div>
        )
    }

    const renderCardTypes = () => {
        const cardTypes = ['Treasure', 'Victory', 'Action', 'Reaction', 'Duration'];

        return (
            cardTypes.map(ct => 
                <ListItem><FormControlLabel key={ct} control={
                    <Checkbox
                        key={ct}
                        name={ct}                                                
                        color="primary"
                        checked={selectedCardTypes.includes(ct)}
                        onChange={onHandleSelectedCardTypeChanged}
                    />
                    }
                    label={ct}
                />  
                </ListItem>
            )
        )
    }

    const isCardTypeSelected = (cardType:string): boolean => {
        return selectedCardTypes.indexOf(cardType) > -1;
    }

    const renderResults = () => {
        if (!sets || sets.length === 0) return <div>No cards found</div>
        let cards = sets.map(s => s.Cards).reduce((a,b) => { return a.concat(b)});
        if (selectedCardTypes.length > 0) {
            cards = cards.filter(x => 
                (x.IsActionCard && isCardTypeSelected("Action")) || 
                (x.IsTreasureCard && isCardTypeSelected("Treasure")) || 
                (x.IsVictoryCard && isCardTypeSelected("Victory")) || 
                (x.IsReactionCard && isCardTypeSelected("Reaction")) || 
                (x.IsDurationCard && isCardTypeSelected("Duration"))        
            );
        }

        if (selectedSets.length > 0) {
            cards = cards.filter(x => selectedSets.indexOf(x.SetName) > -1);
        }

        return (
            <Grid container>
                {cards.map(c => <Grid item sm={2}><CardComponent card={c} /></Grid>)}
            </Grid>
        )       
    }

    return (
        <div>
            <h1>Search</h1>
            <Grid container>
                <Grid item sm={2}>
                    <Paper style={{marginLeft: 30}}>
                        Card Types
                        <List dense={true}>
                            {renderCardTypes()}
                        </List>
                        <Divider />
                        Sets
                        <List dense={true}>
                            {renderSetCheckboxes()}
                        </List>
                    </Paper>
                </Grid>
                <Grid item sm={10}>
                    <Paper>
                        {renderResults()}
                    </Paper>
                </Grid>
            </Grid>
            
            
        </div>
    )
}
export default SearchComponent;