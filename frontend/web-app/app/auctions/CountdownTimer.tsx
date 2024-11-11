import React, { useState, useEffect } from 'react';

const CountdownTimer = ({ targetDate }: { targetDate: Date }) => {
    const [timeLeft, setTimeLeft] = useState(calculateTimeLeft());
    const [completed, setCompleted] = useState(false);

    useEffect(() => {
        const intervalId = setInterval(() => {
            const newTimeLeft = calculateTimeLeft();
            setTimeLeft(newTimeLeft);
            if (!newTimeLeft) {
                setCompleted(true);
                clearInterval(intervalId); // Stop the countdown when completed
            }
        }, 1000);

        return () => clearInterval(intervalId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    function calculateTimeLeft() {
        const difference = targetDate.getTime() - new Date().getTime();
        return difference > 0 ? {
            days: Math.floor(difference / (1000 * 60 * 60 * 24)),
            hours: Math.floor((difference / (1000 * 60 * 60)) % 24),
            minutes: Math.floor((difference / 1000 / 60) % 60),
            seconds: Math.floor((difference / 1000) % 60)
        } : null;
    }

    if (!timeLeft) return <span>Auction finished</span>;


    return (
        <div className={`
            border-2 border-white text-white py-1 px-2 rounded-lg 
            flex justify-center 
            ${completed 
                ? 'bg-red-600' 
                : (timeLeft.days === 0 && timeLeft.hours < 10) 
                    ? 'bg-amber-600' 
                    : 'bg-green-600'}`
            }>
            {completed ? (
                <span>Auction finished</span>
            ) : (
                <span suppressHydrationWarning={true}>
                    {timeLeft.days}:{timeLeft.hours}:{timeLeft.minutes}:{timeLeft.seconds}
                </span>
            )}
        </div>
    )
};

export default CountdownTimer;
